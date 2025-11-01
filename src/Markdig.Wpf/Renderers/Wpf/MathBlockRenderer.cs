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

            try
            {
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

                // Skip empty formulas
                if (string.IsNullOrWhiteSpace(latex))
                {
                    return;
                }

                var control = new WpfMath.Controls.FormulaControl
                {
                    Formula = latex,
                    Scale = 25.0,
                    SystemTextFontName = "Segoe UI",
                    HorizontalAlignment = HorizontalAlignment.Center
                };

                var container = new BlockUIContainer(control)
                {
                    Margin = new Thickness(0, 10, 0, 10)
                };
                renderer.WriteBlock(container);
            }
            catch (Exception ex)
            {
                // Fallback to text rendering if LaTeX parsing fails
                var paragraph = new Paragraph();
                paragraph.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);

                var errorRun = new Run($"[Math Rendering Error: {ex.Message}]\n")
                {
                    Foreground = Brushes.DarkRed,
                    FontWeight = FontWeights.Bold
                };
                paragraph.Inlines.Add(errorRun);

#if DEBUG
                System.Diagnostics.Debug.WriteLine($"Math block rendering error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");

                // Extract and show the LaTeX content
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
                System.Diagnostics.Debug.WriteLine($"LaTeX content: {latexBuilder}");
#endif

                renderer.Push(paragraph);
                renderer.WriteLeafRawLines(obj);
                renderer.Pop();
            }
        }
    }
}
