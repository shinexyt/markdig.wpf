# 快速开始：LaTeX 数学公式语法支持

## 概述

Markdig.Wpf 现在支持两种数学公式语法风格，您可以根据个人喜好选择使用：

### 1. Markdown 风格（原有功能）
```markdown
行内公式：$E = mc^2$

块级公式：
$$
\int_{0}^{\infty} e^{-x^2} dx = \frac{\sqrt{\pi}}{2}
$$
```

### 2. LaTeX 风格（新增功能）?
```latex
行内公式：\(E = mc^2\)

块级公式：
\[
\int_{0}^{\infty} e^{-x^2} dx = \frac{\sqrt{\pi}}{2}
\]
```

## 快速测试

### 方法一：运行示例应用

1. 构建并运行 `Markdig.Wpf.SampleApp` 项目
2. 点击 **"Load LaTeX Test"** 按钮
3. 查看包含 LaTeX 语法的完整数学文档

### 方法二：在您的应用中测试

```csharp
using Markdig.Wpf;

// 创建支持 LaTeX 语法的 pipeline
var pipeline = new MarkdownPipelineBuilder()
    .UseSupportedExtensions()
    .Build();

// 创建 Markdown Viewer
var viewer = new MarkdownViewer 
{ 
    Pipeline = pipeline
};

// 加载包含 LaTeX 语法的文档
viewer.Markdown = @"
# 测试 LaTeX 语法

泰勒公式：函数 \(f(x)\) 在点 \(a\) 附近可以展开为：

\[
f(x) = f(a) + f'(a)(x-a) + \frac{f''(a)}{2!}(x-a)^2 + \dots
\]

著名的欧拉公式：\(e^{i\pi} + 1 = 0\)
";
```

## 您的文本示例

您提供的文本现在可以完美识别和渲染：

```latex
一个多项式来近似复杂的函数。具体来说，假设有一个函数 \(f(x)\)，
且在某点 \(a\) 的附近可以被充分光滑地展开（即存在连续的高阶导数）。
泰勒公式的形式为：

\[ 
f(x) \approx f(a) + f'(a)(x-a) + \frac{f''(a)}{2!}(x-a)^2 + \frac{f'''(a)}{3!}(x-a)^3 + \dots + \frac{f^{(n)}(a)}{n!}(x-a)^n + R_n(x) 
\]

其中：
- \(f^{(n)}(a)\) 表示函数 \(f(x)\) 在点 \(a\) 的第 \(n\) 阶导数。
- \((x-a)^n\) 是 \(x-a\) 的 \(n\) 次幂。
- \(n!\) 是 \(n\) 的阶乘。
- \(R_n(x)\) 是余项，表示在使用 \(n\) 次多项式近似时的误差。
```

## 语法对照表

| 功能 | Markdown 语法 | LaTeX 语法 | 效果 |
|------|--------------|-----------|------|
| 行内公式 | `$...$` | `\(...\)` | 相同 |
| 块级公式 | `$$...$$` | `\[...\]` | 相同 |
| 混合使用 | ? | ? | 完全支持 |

## 常见 LaTeX 命令示例

### 分数
```latex
\(\frac{a}{b}\) 或 \[\frac{numerator}{denominator}\]
```

### 上标和下标
```latex
\(x^2\)、\(x_i\)、\(x^{2n}\)、\(x_{i,j}\)
```

### 求和与积分
```latex
\(\sum_{i=1}^{n} i\)、\(\int_{a}^{b} f(x) dx\)
```

### 希腊字母
```latex
\(\alpha\)、\(\beta\)、\(\gamma\)、\(\Delta\)、\(\Omega\)
```

### 根号
```latex
\(\sqrt{x}\)、\(\sqrt[n]{x}\)
```

### 矩阵
```latex
\[
\begin{bmatrix}
a & b \\
c & d
\end{bmatrix}
\]
```

## 注意事项

1. **自动启用**: 使用 `UseSupportedExtensions()` 会自动启用 LaTeX 语法支持
2. **混合使用**: 可以在同一文档中混合使用两种语法
3. **转义**: 如果需要显示字面的 `\(` 或 `\[`，请使用转义符
4. **错误处理**: 如果公式语法错误，会以红色文本显示而不会崩溃

## 更多资源

- ?? [完整 LaTeX 测试文档](LaTeX-Syntax-Test.md)
- ?? [数学公式演示](Math-Demo.md)
- ?? [详细更新说明](LaTeX-Syntax-Support-Summary.md)
- ?? [WPF-Math 文档](https://github.com/ForNeVeR/wpf-math)
- ?? [LaTeX 数学符号参考](https://en.wikibooks.org/wiki/LaTeX/Mathematics)

## 问题反馈

如果您遇到任何问题或有建议，请在 GitHub 仓库提交 Issue。

---

**现在您可以使用 LaTeX 原生语法书写数学公式了！** ??
