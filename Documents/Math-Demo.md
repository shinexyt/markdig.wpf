# Math/LaTeX Support Demo

This document demonstrates the Math/LaTeX extension support in Markdig.Wpf.

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

## Note

This implementation renders LaTeX as text. For actual visual math rendering, integrate libraries like:
- WPF-Math
- MathJax (in WebView)
- External rendering services
