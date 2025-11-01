// Copyright (c) shinexyt All rights reserved.
// This file is licensed under the MIT license.
// See the LICENSE.md file in the project root for more information.

using Markdig.Extensions.Mathematics;
using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax;

namespace Markdig.Wpf.Extensions
{
    /// <summary>
    /// Parser for LaTeX-style inline math: \(...\) and block math: \[...\]
    /// </summary>
    public class LatexMathInlineParser : InlineParser
    {
        public LatexMathInlineParser()
        {
            OpeningCharacters = new[] { '\\' };
        }

        public override bool Match(InlineProcessor processor, ref StringSlice slice)
        {
   // Check for \( or \[
      if (slice.CurrentChar != '\\')
    return false;

   var start = slice.Start;
     var c = slice.NextChar();
       
   bool isBlock = false;
char openChar, closeChar;

if (c == '(')
      {
      openChar = '(';
closeChar = ')';
    isBlock = false;
    }
else if (c == '[')
   {
     openChar = '[';
    closeChar = ']';
       isBlock = true;
         }
    else
           {
    return false;
 }

     // Move past the opening \( or \[
   slice.Start++;

     var contentStart = slice.Start + 1;
       var contentEnd = contentStart;
       var foundEnd = false;

    // Look for closing \) or \]
   while (!slice.IsEmpty)
       {
      c = slice.CurrentChar;

    if (c == '\\')
    {
    var next = slice.PeekChar(1);
    if (next == closeChar)
    {
          foundEnd = true;
contentEnd = slice.Start - 1;
        break;
     }
     }
   
   slice.NextChar();
    }

 if (!foundEnd)
     {
    // Restore position if we didn't find the end
          slice.Start = start;
    return false;
 }

       // Move past the closing \) or \]
      slice.Start += 2;

        // Extract the math content
       var mathContent = new StringSlice(slice.Text, contentStart, contentEnd);

       // For now, treat both as inline
   var mathInline = new MathInline
        {
   Span = new SourceSpan(start, slice.Start - 1),
Delimiter = '$', // Use standard $ delimiter for compatibility with existing renderers
     DelimiterCount = isBlock ? 2 : 1,
      Content = mathContent
  };

       processor.Inline = mathInline;
  return true;
     }
    }
}
