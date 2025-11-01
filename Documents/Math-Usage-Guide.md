# WPF-Math Integration Usage Guide

This guide explains how to use the WPF-Math integration in Markdig.Wpf for rendering LaTeX mathematical expressions.

## Prerequisites

- .NET Core 3.1, .NET 5.0, or later (WPF-Math requires .NET Framework 4.6.2+)
- WPF-Math package is automatically included for compatible target frameworks

## Basic Usage

### 1. Enable Math Extension

When creating your Markdown pipeline, enable the math extension:

```csharp
using Markdig;
using Markdig.Wpf;

var pipeline = new MarkdownPipelineBuilder()
    .UseSupportedExtensions()  // This includes the math extension
    .Build();

var viewer = new MarkdownViewer
{
    Pipeline = pipeline
};
```

### 2. Write Math Expressions

#### Inline Math
Use single dollar signs `$...$` for inline expressions:

```markdown
The famous equation is $E = mc^2$.
```

#### Block Math
Use double dollar signs `$$...$$` for display expressions:

```markdown
$$
\int_{-\infty}^{\infty} e^{-x^2} dx = \sqrt{\pi}
$$
```

### 3. Load Markdown Content

```csharp
viewer.Markdown = File.ReadAllText("your-document.md");
```

## Supported LaTeX Features

The WPF-Math library supports a wide range of LaTeX features:

### Basic Operations
- Fractions: `\frac{a}{b}`
- Superscripts: `x^2`
- Subscripts: `x_i`
- Square roots: `\sqrt{x}` or `\sqrt[n]{x}`

### Greek Letters
- Lowercase: `\alpha`, `\beta`, `\gamma`, etc.
- Uppercase: `\Delta`, `\Omega`, `\Sigma`, etc.

### Mathematical Operators
- Integrals: `\int`, `\oint`, `\iint`
- Summations: `\sum`
- Products: `\prod`
- Limits: `\lim`

### Matrices
```latex
\begin{bmatrix}
a & b \\
c & d
\end{bmatrix}
```

### Brackets and Delimiters
- Parentheses: `\left( \right)`
- Brackets: `\left[ \right]`
- Braces: `\left\{ \right\}`
- Angle brackets: `\langle \rangle`

## Configuration

### Adjusting Scale

The default scales are:
- Inline math: 20.0
- Block math: 25.0

To customize, you can modify the renderers:

```csharp
// This would require creating custom renderers
// See MathInlineRenderer.cs and MathBlockRenderer.cs for examples
```

### Font Selection

The default system font is "Segoe UI". This is configured in the renderers and can be changed by modifying the `SystemTextFontName` property.

## Error Handling

If a LaTeX expression cannot be parsed:
- The expression will be displayed as text
- The text will be colored in red to indicate an error
- Your application will continue to run without crashing

Example of invalid LaTeX:
```markdown
$\invalid{syntax}$
```

This will render as red text: `$\invalid{syntax}$`

## Target Framework Support

| Target Framework | WPF-Math Support | Rendering Mode |
|-----------------|------------------|----------------|
| .NET Framework 4.5.2 | ? No | Text-based fallback |
| .NET Core 3.1 | ? Yes | Visual WPF-Math rendering |
| .NET 5.0+ | ? Yes | Visual WPF-Math rendering |

## Sample Application

The `Markdig.Wpf.SampleApp` project demonstrates the math rendering capabilities:

1. Run the sample application
2. Click "Load Math Demo" to see mathematical expressions
3. Try "Toggle Extensions" to see the difference with and without extensions

## Troubleshooting

### Math not rendering visually
- **Issue**: Math expressions appear as plain text
- **Solution**: 
  - Ensure you're using .NET Core 3.1 or later
  - Verify the math extension is enabled in your pipeline
  - Check that the WPF-Math package is referenced

### Parse errors
- **Issue**: Some expressions show in red
- **Solution**: 
  - Verify your LaTeX syntax is correct
  - Some LaTeX features may not be supported by WPF-Math
  - Consult the [WPF-Math documentation](https://github.com/ForNeVeR/wpf-math) for supported features

### Performance concerns
- **Issue**: Rendering is slow with many equations
- **Solution**: 
  - Math expressions are rendered on-demand
  - Consider breaking large documents into smaller sections
  - Use virtualization for documents with many equations

## Examples

See `Math-Demo.md` for comprehensive examples of supported LaTeX features.

## Additional Resources

- [WPF-Math GitHub Repository](https://github.com/ForNeVeR/wpf-math)
- [LaTeX Mathematics](https://en.wikibooks.org/wiki/LaTeX/Mathematics)
- [Markdig Documentation](https://github.com/lunet-io/markdig)

## Contributing

To improve math rendering support:
1. Modify `MathInlineRenderer.cs` for inline expressions
2. Modify `MathBlockRenderer.cs` for block expressions
3. Submit pull requests with improvements

Both files are located in `Markdig.Wpf\Renderers\Wpf\`.
