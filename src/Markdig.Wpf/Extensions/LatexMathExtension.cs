// Copyright (c) shinexyt All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Extensions.Mathematics;
using Markdig.Parsers;
using Markdig.Renderers;

namespace Markdig.Wpf.Extensions
{
    /// <summary>
    /// Extension to support LaTeX-style math delimiters: \(...\) for inline and \[...\] for block math.
    /// This works alongside the standard Math extension that uses $ and $$ delimiters.
    /// </summary>
    public class LatexMathExtension : IMarkdownExtension
    {
        public void Setup(MarkdownPipelineBuilder pipeline)
        {
            if (!pipeline.InlineParsers.Contains<LatexMathInlineParser>())
            {
                // Insert before the standard math inline parser if it exists
                var existingMathParser = pipeline.InlineParsers.Find<MathInlineParser>();
                int index = existingMathParser != null ? pipeline.InlineParsers.IndexOf(existingMathParser) : -1;

                if (index >= 0)
                {
                    pipeline.InlineParsers.Insert(index, new LatexMathInlineParser());
                }
                else
                {
                    pipeline.InlineParsers.Add(new LatexMathInlineParser());
                }
            }

            // Note: Block parsers are not needed as LaTeX block math \[...\] 
            // should start on its own line, which is similar to $$ blocks
            // We'll handle this in the inline parser by treating \[ as block start
        }

        public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
        {
            // Renderers are already registered by the standard Math extension
            // Both $ and \( syntax will be rendered the same way
        }
    }
}
