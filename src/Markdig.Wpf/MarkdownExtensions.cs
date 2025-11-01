// Copyright (c) Nicolas Musset. All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using System;

// ReSharper disable once CheckNamespace
namespace Markdig.Wpf
{
    /// <summary>
    /// Provides extension methods for <see cref="MarkdownPipeline"/> to enable several Markdown extensions.
    /// </summary>
    public static class MarkdownExtensions
    {
        /// <summary>
        /// Uses all extensions supported by <c>Markdig.Wpf</c>.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <returns>The modified pipeline</returns>
        public static MarkdownPipelineBuilder UseSupportedExtensions(this MarkdownPipelineBuilder pipeline)
        {
            if (pipeline == null) throw new ArgumentNullException(nameof(pipeline));
            return pipeline
                .UseEmphasisExtras()
                .UseGridTables()
                .UsePipeTables()
                .UseTaskLists()
                .UseAutoLinks()
                .UseMathematics()
                .UseLatexMathematics();
        }

        /// <summary>
        /// Adds support for LaTeX-style math delimiters: \(...\) for inline math and \[...\] for block math.
        /// This adds a secondary math parser that works in parallel with the standard $ and $$ delimiters.
        /// </summary>
        /// <param name="pipeline">The pipeline.</param>
        /// <returns>The modified pipeline</returns>
        public static MarkdownPipelineBuilder UseLatexMathematics(this MarkdownPipelineBuilder pipeline)
        {
            if (pipeline == null) throw new ArgumentNullException(nameof(pipeline));

            // Register a math parser instance with LaTeX-style delimiters
            pipeline.Extensions.AddIfNotAlready(new Extensions.LatexMathExtension());

            return pipeline;
        }
    }
}
