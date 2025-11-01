> [!IMPORTANT]  
> Because of a lack of time and interest, this repo is archived. If my situation changes, I might unarchive it.
> For the time being, consider forking it or use an alternative.
> Existing Nuget packages will still be available.

# Markdig-WPF [![NuGet](https://img.shields.io/nuget/v/Markdig.Wpf.svg?logo=nuget)](https://www.nuget.org/packages/Markdig.wpf/) [![NuGet](https://img.shields.io/nuget/dt/Markdig.Wpf.svg)](https://www.nuget.org/stats/packages/Markdig.Wpf?groupby=Version)
A WPF library for [lunet-io/markdig](https://github.com/lunet-io/markdig)

The project is split into two parts:
- [a WPF renderer](https://github.com/Kryptos-FR/markdig-wpf/blob/master/src/Markdig.Wpf/Renderers/WpfRenderer.cs)
- [a XAML renderer](https://github.com/Kryptos-FR/markdig-wpf/blob/master/src/Markdig.Wpf/Renderers/XamlRenderer.cs)

The WPF renderer allows you to transform markdown text to an equivalent FlowDocument that can then be used in a WPF control. For convenience an implementation of such control is given in [MarkdownViewer](https://github.com/Kryptos-FR/markdig-wpf/blob/master/src/Markdig.Wpf/MarkdownViewer.cs).

The XAML renderer outputs a string in a similar way as the HTML renderer. This string can then be saved into a file or parsed by an application. It is less complete compared to the WPF renderer.

[Markdig.Xaml.SampleApp](https://github.com/Kryptos-FR/markdig-wpf/tree/master/src/Markdig.Xaml.SampleApp) illustrates a way to utilize the parsed XAML at runtime. It should be fine for small documents but might not be the best way for bigger one.


## Features

Supports all standard features from Markdig (i.e. fully CommonMark compliant).

Additionally, the following extensions are supported:
- **Auto-links**
- **Task lists** (WPF renderer only)
- **Tables** (partial support of grid and pipe tables) (WPF renderer only)
- **Extra emphasis**
- **Mathematics/LaTeX** (inline `$...$` and block `$$...$$` expressions with WPF-Math visual rendering)

### Mathematics/LaTeX Support

The Math extension allows you to render LaTeX mathematical expressions in your WPF applications with **visual rendering powered by WPF-Math**:

- **Inline math**: Use single dollar signs `$...$` for inline expressions like `$E = mc^2$`
- **Block math**: Use double dollar signs `$$...$$` for display expressions on their own lines

**Example:**
```markdown
The quadratic formula is $x = \frac{-b \pm \sqrt{b^2 - 4ac}}{2a}$.

Block math:
$$
\int_{-\infty}^{\infty} e^{-x^2} dx = \sqrt{\pi}
$$
```

**Rendering Implementation:**
- **For .NET Core 3.1 and .NET 5.0+**: Math expressions are rendered visually using the [WPF-Math](https://github.com/ForNeVeR/wpf-math) library (version 2.1.0), providing professional-quality mathematical typesetting
- **For .NET Framework 4.5.2**: Math expressions are rendered as styled text (similar to code blocks) due to WPF-Math compatibility requirements

**Features:**
- Supports a wide range of LaTeX mathematical symbols and expressions
- Properly formatted fractions, integrals, summations, matrices, and more
- Greek letters and mathematical operators
- Error handling with fallback to text rendering for invalid LaTeX

**Note:** If you encounter issues with specific LaTeX expressions, they will fall back to text rendering with error indication.

See [Math-Demo.md](Documents/Math-Demo.md) for more examples and demonstrations.
