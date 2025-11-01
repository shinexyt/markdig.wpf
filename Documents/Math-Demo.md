# Math/LaTeX Support Demo

This document demonstrates the Math/LaTeX extension support in Markdig.Wpf with **WPF-Math visual rendering**.

## Visual Rendering

This implementation uses [WPF-Math](https://github.com/ForNeVeR/wpf-math) library to provide professional-quality mathematical typesetting directly in WPF applications. Math expressions are rendered as visual elements rather than plain text.

**Supported Targets:**
- ✅ **.NET Framework 4.6.2+** 
- ✅ **.NET 8.0**

## Inline Math

You can use inline math expressions like this: $E = mc^2$ or $\pi \approx 3.14159$.

Here's another example with inline math: The quadratic formula is $x = \frac{-b \pm \sqrt{b^2 - 4ac}}{2a}$.

## Block Math

Block math expressions are displayed on their own lines:

$$
\int_{-\infty}^{\infty} e^{-x^2} dx = \sqrt{\pi}
$$

Here's Euler's identity:

$$
e^{i\pi} + 1 = 0
$$

The Pythagorean theorem:

$$
a^2 + b^2 = c^2
$$

## More Complex Examples

### Matrix

$$
\begin{bmatrix}
a & b \\
c & d
\end{bmatrix}
$$

### Sum Notation

$$
\sum_{i=1}^{n} i = \frac{n(n+1)}{2}
$$

### Greek Letters

Inline: $\alpha$, $\beta$, $\gamma$, $\Delta$, $\Omega$

Block:

$$
\alpha + \beta = \gamma
$$

## Error Handling

If a LaTeX expression cannot be parsed (invalid syntax), the renderer will display the expression as text with a red color to indicate an error, ensuring your application doesn't crash.

Example of potential error (if syntax is incorrect):
$\invalid{syntax}$

## Implementation Details

The math rendering is implemented using:
- **WpfMath.Controls.FormulaControl** for direct visual rendering
- **Conditional compilation** with `USE_WPFMATH` define for all target frameworks
- **Error handling** to gracefully handle invalid LaTeX expressions
- **Configurable scaling** for inline (20.0) and block (25.0) math displays

### Technical Notes

1. **Inline Math**: Rendered using `InlineUIContainer` with `FormulaControl`
2. **Block Math**: Rendered using `BlockUIContainer` with centered `FormulaControl`
3. **Fallback**: Invalid expressions render as styled text with error indication
4. **Performance**: Math expressions are rendered on-demand during document processing
5. **Cross-Framework**: Works identically on .NET Framework 4.6.2 and .NET 8.0
