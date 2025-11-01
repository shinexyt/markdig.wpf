# 数学公式语法快速参考

## 分隔符速查表

| 用途 | Markdown 语法 | LaTeX 语法 | 示例 |
|------|--------------|-----------|------|
| 行内公式 | `$...$` | `\(...\)` | `$E = mc^2$` |
| 块级公式 | `$$...$$` | `\[...\]` | `$$\int f(x) dx$$` |

## 常用命令速查

### 基础运算

| 功能 | 命令 | 示例 | 效果 |
|------|-----|------|------|
| 分数 | `\frac{分子}{分母}` | `\frac{a}{b}` | a/b |
| 上标 | `^` | `x^2` | x? |
| 下标 | `_` | `x_i` | x? |
| 根号 | `\sqrt{x}` | `\sqrt{x}` | √x |
| n次根 | `\sqrt[n]{x}` | `\sqrt[3]{x}` | ?√x |

### 运算符

| 功能 | 命令 | 示例 |
|------|-----|------|
| 求和 | `\sum_{下标}^{上标}` | `\sum_{i=1}^{n} x_i` |
| 积分 | `\int_{下限}^{上限}` | `\int_{0}^{1} f(x) dx` |
| 乘积 | `\prod_{下标}^{上标}` | `\prod_{i=1}^{n} x_i` |
| 极限 | `\lim_{变量 \to 值}` | `\lim_{x \to 0} f(x)` |

### 希腊字母

| 小写 | 命令 | 大写 | 命令 |
|------|-----|------|-----|
| α | `\alpha` | Γ | `\Gamma` |
| β | `\beta` | Δ | `\Delta` |
| γ | `\gamma` | Θ | `\Theta` |
| δ | `\delta` | Λ | `\Lambda` |
| ε | `\epsilon` | Ξ | `\Xi` |
| θ | `\theta` | Π | `\Pi` |
| λ | `\lambda` | Σ | `\Sigma` |
| μ | `\mu` | Φ | `\Phi` |
| π | `\pi` | Ψ | `\Psi` |
| σ | `\sigma` | Ω | `\Omega` |

### 符号

| 功能 | 命令 | 功能 | 命令 |
|------|-----|------|-----|
| 无穷 | `\infty` | 偏导 | `\partial` |
| 省略号 | `\cdots` | 点乘 | `\cdot` |
| 下省略号 | `\ldots` | 叉乘 | `\times` |
| 不等于 | `\neq` | 约等于 | `\approx` |
| 小于等于 | `\leq` | 大于等于 | `\geq` |
| 属于 | `\in` | 不属于 | `\notin` |
| 子集 | `\subset` | 交集 | `\cap` |
| 并集 | `\cup` | 空集 | `\emptyset` |

### 括号

| 类型 | 命令 | 自动调整大小 |
|------|-----|-------------|
| 圆括号 | `( )` | `\left( \right)` |
| 方括号 | `[ ]` | `\left[ \right]` |
| 花括号 | `\{ \}` | `\left\{ \right\}` |
| 角括号 | `\langle \rangle` | `\left\langle \right\rangle` |
| 绝对值 | `| |` | `\left| \right|` |
| 范数 | `\| \|` | `\left\| \right\|` |

### 矩阵

```latex
\begin{matrix}  % 无边框
a & b \\
c & d
\end{matrix}

\begin{pmatrix}   % 圆括号
a & b \\
c & d
\end{pmatrix}

\begin{bmatrix}   % 方括号
a & b \\
c & d
\end{bmatrix}

\begin{vmatrix}   % 行列式
a & b \\
c & d
\end{vmatrix}
```

## 常见错误及修正

### ? 错误示例

| 错误写法 | 问题 |
|---------|-----|
| `**f(x) = x^2**` | 使用了 Markdown 粗体而不是数学分隔符 |
| `$x?$` | 使用 Unicode 上标而不是 LaTeX 语法 |
| `$f(x)/2!$` | 分数应使用 \frac 命令 |
| `$...` | 省略号应使用 \cdots 或 \dots |

### ? 正确示例

| 正确写法 | 说明 |
|---------|-----|
| `$f(x) = x^2$` | 使用 $ 分隔符 |
| `$x^2$` | 使用 ^ 表示上标 |
| `$\frac{f(x)}{2!}$` | 使用 \frac 命令 |
| `$a, b, \cdots, z$` | 使用 \cdots |

## 完整示例

### 泰勒公式

```markdown
函数 \(f(x)\) 在点 \(a\) 的泰勒展开：

$$
f(x) = f(a) + f'(a)(x-a) + \frac{f''(a)}{2!}(x-a)^2 + \frac{f'''(a)}{3!}(x-a)^3 + \cdots
$$

或使用求和表示：

\[
f(x) = \sum_{n=0}^{\infty} \frac{f^{(n)}(a)}{n!}(x-a)^n
\]
```

### 常见函数展开

```markdown
$$
e^x = \sum_{n=0}^{\infty} \frac{x^n}{n!} = 1 + x + \frac{x^2}{2!} + \frac{x^3}{3!} + \cdots
$$

$$
\sin(x) = \sum_{n=0}^{\infty} \frac{(-1)^n}{(2n+1)!}x^{2n+1}
$$

$$
\ln(1+x) = \sum_{n=1}^{\infty} \frac{(-1)^{n+1}}{n}x^n, \quad |x| < 1
$$
```

## 调试技巧

### 1. 逐步测试
从简单公式开始，逐步添加复杂内容：

```markdown
$x$           ← 先测试最简单的
$x^2$         ← 添加上标
$\frac{x^2}{2}$ ← 添加分数
```

### 2. 检查分隔符配对
确保所有分隔符正确配对：
- `$` 和 `$`
- `$$` 和 `$$`
- `\(` 和 `\)`
- `\[` 和 `\]`
- `{` 和 `}`

### 3. 查看错误提示
在 Debug 模式下，控制台会显示详细错误信息。

### 4. 参考示例
运行示例应用查看正确的渲染效果：
- **Math Demo** - 基础数学公式
- **LaTeX Test** - LaTeX 语法示例
- **Taylor Formula** - 完整泰勒公式示例

## 快速检查清单

编写数学公式前，检查以下项目：

- [ ] 使用了正确的分隔符（`$`、`$$`、`\(`、`\[`）
- [ ] 没有混用 Markdown 格式标记（如 `**`、`_`）
- [ ] 上标使用 `^`，下标使用 `_`
- [ ] 分数使用 `\frac{}{}`
- [ ] 括号需要时使用 `\left` 和 `\right`
- [ ] 特殊符号使用 LaTeX 命令（如 `\infty`、`\alpha`）
- [ ] 省略号使用 `\cdots` 或 `\dots`

## 资源链接

- [完整错误处理指南](Math-Rendering-Error-Guide.md)
- [泰勒公式正确示例](Taylor-Formula-Correct.md)
- [LaTeX 语法测试](LaTeX-Syntax-Test.md)
- [LaTeX 数学符号大全](https://en.wikibooks.org/wiki/LaTeX/Mathematics)
- [WPF-Math 文档](https://github.com/ForNeVeR/wpf-math)

---

**提示**: 将此文档保存为书签，方便随时查阅！
