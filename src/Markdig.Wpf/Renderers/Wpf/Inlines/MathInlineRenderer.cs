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

            var latex = obj.Content.ToString();
      
            // Early return for empty content
         if (string.IsNullOrWhiteSpace(latex))
            {
          WriteFallback(renderer, "$" + latex + "$");
      return;
            }

 try
   {
     // Create the control - the Formula property setter will validate
 // We catch any exceptions to prevent UI thread freezing during streaming
  var control = new WpfMath.Controls.FormulaControl
  {
     Scale = 20.0,
        SystemTextFontName = "Segoe UI"
 };
  
   // Set the formula - this is where TexParseException can be thrown
      control.Formula = latex;

         renderer.WriteInline(new InlineUIContainer(control));
         }
  catch (Exception ex)
      {
                // Fallback to text rendering if LaTeX parsing fails
       // This prevents application freeze when streaming invalid/partial LaTeX
     System.Diagnostics.Debug.WriteLine($"Math rendering error: {ex.Message}");
      WriteFallback(renderer, "$" + latex + "$");
      }
     }

      private void WriteFallback(WpfRenderer renderer, string text)
        {
          var run = new Run(text)
    {
       Foreground = Brushes.DarkRed
        };
  run.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeStyleKey);
            renderer.WriteInline(run);
   }
    }
}
