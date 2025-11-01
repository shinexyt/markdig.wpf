// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using Markdig.Extensions.Mathematics;

namespace Markdig.Renderers.Xaml
{
    /// <summary>
    /// A XAML renderer for a <see cref="MathBlock"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{MathBlock}" />
    public class MathBlockRenderer : XamlObjectRenderer<MathBlock>
    {
        protected override void Write(XamlRenderer renderer, MathBlock obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            renderer.EnsureLine();

            renderer.Write("<Paragraph xml:space=\"preserve\"");
            // Apply code block styling for math blocks
            renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.CodeBlockStyleKey}}\"");
            renderer.WriteLine(">");
            renderer.WriteLeafRawLines(obj, true, true);
            renderer.WriteLine("</Paragraph>");
        }
    }
}
