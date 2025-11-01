# LaTeX 语法支持更新总结

## 更新概述

本次更新为 Markdig.Wpf 添加了对 **LaTeX 原生数学公式语法** 的支持，使其能够同时识别 Markdown 和 LaTeX 两种风格的数学公式分隔符。

## 问题背景

用户发现以下使用 LaTeX 原生语法的数学公式无法被识别：

```latex
一个多项式来近似复杂的函数。具体来说，假设有一个函数 \(f(x)\)，
且在某点 \(a\) 的附近可以被充分光滑地展开。泰勒公式的形式为：

\[ f(x) \approx f(a) + f'(a)(x-a) + \frac{f''(a)}{2!}(x-a)^2 + ... \]
```

原因：之前的实现只支持 Markdown 风格的 `$...$` 和 `$$...$$` 语法。

## 解决方案

### 1. 新增自定义扩展

创建了 `LatexMathExtension` 类来添加 LaTeX 语法支持：

**文件**: `Markdig.Wpf\Extensions\LatexMathExtension.cs`

- 实现 `IMarkdownExtension` 接口
- 注册自定义的 LaTeX 数学解析器
- 与现有的 Mathematics 扩展和平共存

### 2. 实现 LaTeX 解析器

创建了 `LatexMathInlineParser` 类来解析 LaTeX 语法：

**文件**: `Markdig.Wpf\Extensions\LatexMathInlineParser.cs`

**支持的语法**:
- `\(...\)` - 行内数学公式
- `\[...\]` - 块级数学公式（显示公式）

**工作原理**:
1. 检测反斜杠 `\` 字符
2. 识别开括号 `(` 或 `[`
3. 查找对应的闭括号 `\)` 或 `\]`
4. 提取公式内容
5. 创建 `MathInline` 对象供渲染器处理

### 3. 更新扩展方法

修改了 `MarkdownExtensions.cs`:

```csharp
public static MarkdownPipelineBuilder UseSupportedExtensions(this MarkdownPipelineBuilder pipeline)
{
    return pipeline
        .UseEmphasisExtras()
        .UseGridTables()
      .UsePipeTables()
    .UseTaskLists()
        .UseAutoLinks()
    .UseMathematics()      // 标准 $ 语法
        .UseLatexMathematics(); // 新增: LaTeX \( 语法
}
```

### 4. 示例应用增强

**更新的文件**:
- `Markdig.Wpf.SampleApp\MainWindow.xaml` - 添加 "Load LaTeX Test" 按钮
- `Markdig.Wpf.SampleApp\MainWindow.xaml.cs` - 添加按钮事件处理
- `Markdig.Wpf.SampleApp\Markdig.Wpf.SampleApp.csproj` - 包含测试文档

### 5. 测试文档

创建了 `LaTeX-Syntax-Test.md` 作为测试文档：
- 包含完整的泰勒公式示例
- 展示 LaTeX 语法的各种用法
- 对比 Markdown 和 LaTeX 两种语法

## 支持的语法对照

| 类型 | Markdown 语法 | LaTeX 语法 | 说明 |
|------|--------------|-----------|------|
| 行内公式 | `$E = mc^2$` | `\(E = mc^2\)` | 两种语法等效 |
| 块级公式 | `$$\int f(x) dx$$` | `\[\int f(x) dx\]` | 两种语法等效 |

**重要提示**: 两种语法可以在同一文档中混合使用！

## 技术细节

### 解析器优先级

LaTeX 解析器被插入到标准 Math 解析器**之前**，确保：
1. LaTeX 语法被优先识别
2. 不影响现有的 `$` 语法支持
3. 两种语法可以共存

### 渲染兼容性

- LaTeX 语法解析后创建标准的 `MathInline` 对象
- 使用现有的 `MathInlineRenderer` 和 `MathBlockRenderer` 进行渲染
- 无需修改渲染逻辑，完全兼容

### 性能考虑

- 解析器仅在遇到 `\` 字符时激活
- 快速失败机制：如果不是数学公式，立即返回
- 不影响其他内容的解析性能

## 使用方法

### 默认配置（推荐）

使用 `UseSupportedExtensions()` 自动启用所有功能：

```csharp
var pipeline = new MarkdownPipelineBuilder()
    .UseSupportedExtensions()  // 自动包含 LaTeX 语法支持
    .Build();

var viewer = new MarkdownViewer 
{ 
    Pipeline = pipeline,
    Markdown = markdownContent
};
```

### 手动配置

如果需要精细控制：

```csharp
var pipeline = new MarkdownPipelineBuilder()
    .UseMathematics()    // 标准 $ 语法
    .UseLatexMathematics()    // LaTeX \( 语法
    .Build();
```

## 测试验证

### 构建状态
? 编译成功，无错误

### 测试覆盖
- ? 行内公式 `\(...\)`
- ? 块级公式 `\[...\]`
- ? 复杂公式（分数、积分、求和等）
- ? 希腊字母
- ? 矩阵
- ? 与 Markdown 语法混合使用

### 示例应用
运行 `Markdig.Wpf.SampleApp` 并点击 "Load LaTeX Test" 按钮可查看完整示例。

## 文档更新

- ? `Math-Demo.md` - 更新说明双语法支持
- ? `LaTeX-Syntax-Test.md` - 新增 LaTeX 语法完整示例
- ? `README.md` - 更新特性列表和示例
- ? 本文档 - 更新总结

## 向后兼容性

? **完全向后兼容**
- 现有使用 `$` 和 `$$` 的文档无需修改
- LaTeX 语法作为额外功能添加
- 不影响任何现有功能

## 已知限制

1. **块级公式上下文**: `\[...\]` 在技术上应该单独占行，但当前实现将其视为增强的行内元素
2. **转义处理**: 反斜杠 `\` 的转义可能需要特殊处理（如 `\\`）
3. **嵌套限制**: 不支持嵌套的数学分隔符

## 未来改进方向

1. **独立块级解析器**: 为 `\[...\]` 创建真正的块级解析器
2. **更好的错误提示**: 提供更详细的语法错误信息
3. **性能优化**: 对频繁出现的公式进行缓存
4. **可配置性**: 允许用户禁用某种语法

## 总结

本次更新成功添加了对 LaTeX 原生数学公式语法的支持，使 Markdig.Wpf 能够处理更多样化的数学文档格式。用户现在可以：

1. ? 使用 LaTeX 标准的 `\(...\)` 和 `\[...\]` 语法
2. ? 在同一文档中混合使用 Markdown 和 LaTeX 语法
3. ? 无缝迁移包含 LaTeX 公式的现有文档
4. ? 享受 WPF-Math 提供的专业数学排版

**完全解决了用户提出的问题！**
