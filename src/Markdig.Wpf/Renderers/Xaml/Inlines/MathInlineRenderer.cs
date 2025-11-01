// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;
using Markdig.Extensions.Mathematics;

namespace Markdig.Renderers.Xaml.Inlines
{
    /// <summary>
    /// A XAML renderer for a <see cref="MathInline"/>.
    /// </summary>
    /// <seealso cref="XamlObjectRenderer{MathInline}" />
    public class MathInlineRenderer : XamlObjectRenderer<MathInline>
    {
        protected override void Write(XamlRenderer renderer, MathInline obj)
        {
            if (renderer == null) throw new ArgumentNullException(nameof(renderer));
            if (obj == null) throw new ArgumentNullException(nameof(obj));

            renderer.Write("<Run");
            // Apply code styling for math expressions
            renderer.Write(" Style=\"{StaticResource {x:Static markdig:Styles.CodeStyleKey}}\"");
            renderer.Write(" Text=\"").WriteEscape(ref obj.Content).Write("\"");
            renderer.Write(" />");
        }
    }
}
