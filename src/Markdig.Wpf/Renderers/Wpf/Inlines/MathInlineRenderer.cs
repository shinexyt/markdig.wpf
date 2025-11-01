// Copyright (c) shinexyt All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Markdig.Extensions.Mathematics;
using Markdig.Wpf;

namespace Markdig.Renderers.Wpf.Inlines
{
    /// <summary>
    /// A WPF renderer for a <see cref="MathInline"/>.
    /// </summary>
    /// <seealso cref="WpfObjectRenderer{MathInline}" />
    public class MathInlineRenderer : WpfObjectRenderer<MathInline>
    {
        protected override void Write(WpfRenderer renderer, MathInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            try
            {
                var latex = obj.Content.ToString();
                
                // Skip empty formulas
                if (string.IsNullOrWhiteSpace(latex))
                {
                    return;
                }

                var control = new WpfMath.Controls.FormulaControl
                {
                    Formula = latex,
                    Scale = 20.0,
                    SystemTextFontName = "Segoe UI"
                };

                renderer.WriteInline(new InlineUIContainer(control));
            }
            catch (Exception ex)
            {
                // Fallback to text rendering if LaTeX parsing fails
                // Show the error in a more informative way during development
                var errorText = $"${obj.Content.ToString()}$";
                
#if DEBUG
                // In debug mode, show the actual error
                System.Diagnostics.Debug.WriteLine($"Math rendering error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"LaTeX content: {obj.Content.ToString()}");
#endif

                var run = new Run(errorText)
                {
                    Foreground = Brushes.DarkRed,
                    ToolTip = $"Math rendering error: {ex.Message}"
                };
                run.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeStyleKey);
                renderer.WriteInline(run);
            }
        }
    }
}
