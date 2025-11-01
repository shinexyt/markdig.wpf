// Copyright (c) shinexyt All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Markdig.Extensions.Mathematics;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf
{
    /// <summary>
    /// A WPF renderer for a <see cref="MathBlock"/>.
    /// </summary>
    /// <seealso cref="WpfObjectRenderer{MathBlock}" />
    public class MathBlockRenderer : WpfObjectRenderer<MathBlock>
    {
        protected override void Write(WpfRenderer renderer, MathBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            // Extract LaTeX content from the block
            var latexBuilder = new StringBuilder();
            if (obj.Lines.Lines != null)
            {
                var lines = obj.Lines;
                var slices = lines.Lines;
                for (var i = 0; i < lines.Count; i++)
                {
                    if (i > 0) latexBuilder.AppendLine();
                    latexBuilder.Append(slices[i].Slice.ToString());
                }
            }

            var latex = latexBuilder.ToString().Trim();

            // Early return for empty content
            if (string.IsNullOrWhiteSpace(latex))
            {
                WriteFallback(renderer, obj, "Empty math block");
                return;
            }

            try
            {
                // Create the control - the Formula property setter will validate
                // We catch any exceptions to prevent UI thread freezing during streaming
                var control = new WpfMath.Controls.FormulaControl
                {
                    Scale = 25.0,
                    SystemTextFontName = "Segoe UI",
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                // Set the formula - this is where TexParseException can be thrown
                control.Formula = latex;

                var container = new BlockUIContainer(control)
                {
                    Margin = new Thickness(0, 10, 0, 10)
                };
                renderer.WriteBlock(container);
            }
            catch (Exception ex)
            {
                // Fallback to text rendering if LaTeX parsing fails
                // This prevents application freeze when streaming invalid/partial LaTeX
                System.Diagnostics.Debug.WriteLine($"Math block rendering error: {ex.Message}");
                WriteFallback(renderer, obj, ex.Message);
            }
        }

        private void WriteFallback(WpfRenderer renderer, MathBlock obj, string errorMessage)
        {
            var paragraph = new Paragraph();
            paragraph.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);

            var errorRun = new Run($"[Math Error: {errorMessage}]\n")
            {
                Foreground = Brushes.DarkRed,
                FontWeight = FontWeights.Bold
            };
            paragraph.Inlines.Add(errorRun);

            renderer.Push(paragraph);
            renderer.WriteLeafRawLines(obj);
            renderer.Pop();
        }
    }
}
