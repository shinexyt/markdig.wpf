# Markdig-WPF [![NuGet](https://img.shields.io/nuget/v/Shinexyt.Markdig.Wpf.svg?logo=nuget)](https://www.nuget.org/packages/Shinexyt.Markdig.Wpf/)

> **Note:** This repository is forked from [Kryptos-FR/markdig-wpf](https://github.com/Kryptos-FR/markdig-wpf)
> 
> **Key Enhancement:** This fork adds **Mathematics/LaTeX support** with visual rendering powered by WPF-Math, supporting both Markdown-style (`$...$`, `$$...$$`) and LaTeX-style (`\(...\)`, `\[...\]`) delimiters for mathematical expressions in your WPF applications. See [Math-Demo.md](Documents/Math-Demo.md) and [LaTeX-Syntax-Test.md](Documents/LaTeX-Syntax-Test.md) for examples and demonstrations.

A WPF library for [xoofx/markdig](https://github.com/xoofx/markdig)

The project is split into two parts:
- [a WPF renderer](https://github.com/shinexyt/markdig-wpf/blob/master/src/Markdig.Wpf/Renderers/WpfRenderer.cs)
- [a XAML renderer](https://github.com/shinexyt/markdig-wpf/blob/master/src/Markdig.Wpf/Renderers/XamlRenderer.cs)

The WPF renderer allows you to transform markdown text to an equivalent FlowDocument that can then be used in a WPF control. For convenience an implementation of such control is given in [MarkdownViewer](https://github.com/Kryptos-FR/markdig-wpf/blob/master/src/Markdig.Wpf/MarkdownViewer.cs).

The XAML renderer outputs a string in a similar way as the HTML renderer. This string can then be saved into a file or parsed by an application. It is less complete compared to the WPF renderer.

[Markdig.Xaml.SampleApp](https://github.com/shinexyt/markdig-wpf/tree/master/src/Markdig.Xaml.SampleApp) illustrates a way to utilize the parsed XAML at runtime. It should be fine for small documents but might not be the best way for bigger one.


## Features

Supports all standard features from Markdig (i.e. fully CommonMark compliant).

Additionally, the following extensions are supported:
- **Auto-links**
- **Task lists** (WPF renderer only)
- **Tables** (partial support of grid and pipe tables) (WPF renderer only)
- **Extra emphasis**
- **Mathematics/LaTeX** with dual delimiter support:
  - **Markdown style**: `$...$` (inline) and `$$...$$` (block)
  - **LaTeX style**: `\(...\)` (inline) and `\[...\]` (block)
  - Powered by WPF-Math for professional mathematical typesetting
  - Both syntaxes can be mixed in the same document

## Math/LaTeX Examples

### Markdown Style
```markdown
Inline: The famous equation is $E = mc^2$.

Block:
$$
\int_{-\infty}^{\infty} e^{-x^2} dx = \sqrt{\pi}
$$
```

### LaTeX Style
```markdown
Inline: The famous equation is \(E = mc^2\).

Block:
\[
\int_{-\infty}^{\infty} e^{-x^2} dx = \sqrt{\pi}
\]
```

Both render identically with professional mathematical typesetting!
