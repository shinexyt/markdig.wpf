// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license. 
// See the LICENSE.md file in the project root for more information.

using System;
using System.Windows;
using System.Windows.Documents;
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

            var paragraph = new Paragraph();
            paragraph.SetResourceReference(FrameworkContentElement.StyleProperty, Styles.CodeBlockStyleKey);
            renderer.Push(paragraph);
            renderer.WriteLeafRawLines(obj);
            renderer.Pop();
        }
    }
}
