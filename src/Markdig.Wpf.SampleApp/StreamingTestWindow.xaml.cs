using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Markdig;

namespace Markdig.Wpf.SampleApp
{
    public partial class StreamingTestWindow : Window
    {
        private MarkdownPipeline _pipeline;
        private CancellationTokenSource _cancellationTokenSource;
        private Stopwatch _stopwatch;
        private bool _isStreaming;

      // 测试内容 - 包含中文和LaTeX数学表达式
      private const string TEST_CONTENT = @"# 泰勒公式（Taylor Formula）测试

泰勒公式是数学中一种关键的方法，用于将一个函数在某点附近以级数形式展开。它广泛应用于数学分析、数值计算以及物理等领域。

## 基本定义

假设函数 \(f(x)\) 在点 \(a\) 的某个邻域内是可微且连续的，那么可以将 \(f(x)\) 展开为泰勒级数：

\[
f(x) = f(a) + f'(a)(x-a) + \frac{f''(a)}{2!}(x-a)^2 + \frac{f^{(3)}(a)}{3!}(x-a)^3 + \cdots + \frac{f^{(n)}(a)}{n!}(x-a)^n + R_n(x)
\]

其中：
- \(f^{(n)}(a)\) 表示 \(f(x)\) 在 \(x=a\) 处的第 \(n\) 阶导数
- \((x-a)^n\) 是 \(x\) 与展开点 \(a\) 的差的 \(n\) 次方
- \(n!\) 是 \(n\) 的阶乘，是从 1 到 \(n\) 的所有整数的乘积
- \(R_n(x)\) 是余项，用于表示展开的误差

## 特殊形式

### 麦克劳林公式

当 \(a=0\) 时，泰勒公式称为**麦克劳林公式**：

\[
f(x) = f(0) + f'(0)x + \frac{f''(0)}{2!}x^2 + \frac{f^{(3)}(0)}{3!}x^3 + \cdots
\]

### 指数函数展开

例如，指数函数 \(e^x\) 的麦克劳林展开：

\[
e^x = 1 + x + \frac{x^2}{2!} + \frac{x^3}{3!} + \frac{x^4}{4!} + \cdots = \sum_{n=0}^{\infty} \frac{x^n}{n!}
\]

### 三角函数展开

正弦函数的泰勒展开：

\[
\sin(x) = x - \frac{x^3}{3!} + \frac{x^5}{5!} - \frac{x^7}{7!} + \cdots = \sum_{n=0}^{\infty} \frac{(-1)^n x^{2n+1}}{(2n+1)!}
\]

余弦函数的泰勒展开：

\[
\cos(x) = 1 - \frac{x^2}{2!} + \frac{x^4}{4!} - \frac{x^6}{6!} + \cdots = \sum_{n=0}^{\infty} \frac{(-1)^n x^{2n}}{(2n)!}
\]

## 应用场景

### 1. 函数逼近

在实际计算中，泰勒公式可以用有限阶的级数来近似复杂函数。比如计算 \(\sin(0.1)\) 可以使用：

\[
\sin(0.1) \approx 0.1 - \frac{(0.1)^3}{6} + \frac{(0.1)^5}{120} \approx 0.0998334
\]

### 2. 欧拉公式

利用泰勒级数可以推导出著名的欧拉公式：

\[
e^{i\theta} = \cos(\theta) + i\sin(\theta)
\]

当 \(\theta = \pi\) 时，得到欧拉恒等式：

\[
e^{i\pi} + 1 = 0
\]

### 3. 微积分应用

利用泰勒公式求极限。例如：

\[
\lim_{x \to 0} \frac{\sin(x) - x}{x^3} = \lim_{x \to 0} \frac{-\frac{x^3}{6} + O(x^5)}{x^3} = -\frac{1}{6}
\]

### 4. 数值计算

计算 \(e\) 的近似值：

\[
e \approx 1 + 1 + \frac{1}{2} + \frac{1}{6} + \frac{1}{24} + \frac{1}{120} \approx 2.71667
\]

## 余项估计

拉格朗日余项公式：

\[
R_n(x) = \frac{f^{(n+1)}(\xi)}{(n+1)!}(x-a)^{n+1}
\]

其中 \(\xi\) 是 \(a\) 和 \(x\) 之间的某个值。

## 多变量泰勒公式

对于二元函数 \(f(x, y)\)，在点 \((a, b)\) 处的泰勒展开：

\[
f(x, y) = f(a, b) + \frac{\partial f}{\partial x}(a,b)(x-a) + \frac{\partial f}{\partial y}(a,b)(y-b) + \cdots
\]

二阶项：

\[
+ \frac{1}{2!}\left[\frac{\partial^2 f}{\partial x^2}(x-a)^2 + 2\frac{\partial^2 f}{\partial x \partial y}(x-a)(y-b) + \frac{\partial^2 f}{\partial y^2}(y-b)^2\right]
\]

## 总结

泰勒公式是数学分析中的重要工具，通过多项式逼近的方式，将复杂函数转化为易于计算的形式。关键优势：

1. **精度可控**：通过增加展开阶数提高精度
2. **计算简化**：多项式计算比原函数更简单
3. **理论价值**：证明许多重要数学定理的基础
4. **实用性强**：在科学计算、工程应用中广泛使用

最后，记住二项式定理也是泰勒公式的特例：

\[
(1 + x)^n = 1 + nx + \frac{n(n-1)}{2!}x^2 + \frac{n(n-1)(n-2)}{3!}x^3 + \cdots
\]

---

**测试完成！** 如果你能看到上面所有的数学公式都正确渲染，说明流式传输功能工作正常！
";

        public StreamingTestWindow()
{
            InitializeComponent();
       InitializePipeline();
       SetupEventHandlers();
        }

        private void InitializePipeline()
        {
 _pipeline = new MarkdownPipelineBuilder()
          .UseSupportedExtensions()
    .Build();
            DataContext = new { Pipeline = _pipeline };
        }

        private void SetupEventHandlers()
        {
        SpeedSlider.ValueChanged += (s, e) =>
        {
     SpeedLabel.Text = $"{(int)SpeedSlider.Value} ms";
            };
        }

        private async void StartStreaming_Click(object sender, RoutedEventArgs e)
        {
     await StartStreaming((int)SpeedSlider.Value);
        }

    private async void FastStreaming_Click(object sender, RoutedEventArgs e)
   {
   SpeedSlider.Value = 10;
  await StartStreaming(10);
        }

 private async void SlowStreaming_Click(object sender, RoutedEventArgs e)
        {
         SpeedSlider.Value = 200;
         await StartStreaming(200);
        }

        private async Task StartStreaming(int delayMs)
  {
            if (_isStreaming)
         {
     MessageBox.Show("流式传输正在进行中...", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
       return;
       }

   try
  {
            _isStreaming = true;
           StartStreamingButton.IsEnabled = false;
      StopStreamingButton.IsEnabled = true;
    SpeedSlider.IsEnabled = false;

    _cancellationTokenSource = new CancellationTokenSource();
    _stopwatch = Stopwatch.StartNew();

                StatusLabel.Text = "? 正在流式传输...";
        StatusLabel.Foreground = System.Windows.Media.Brushes.Orange;

    await StreamContent(TEST_CONTENT, delayMs, _cancellationTokenSource.Token);

      if (!_cancellationTokenSource.Token.IsCancellationRequested)
     {
        StatusLabel.Text = "? 传输完成";
         StatusLabel.Foreground = System.Windows.Media.Brushes.Green;
       MessageBox.Show($"流式传输完成！\n\n" +
               $"总字符数: {TEST_CONTENT.Length}\n" +
    $"耗时: {_stopwatch.Elapsed.TotalSeconds:F2} 秒\n" +
  $"平均速度: {TEST_CONTENT.Length / _stopwatch.Elapsed.TotalSeconds:F0} 字符/秒",
  "完成",
  MessageBoxButton.OK,
  MessageBoxImage.Information);
       }
}
     catch (Exception ex)
            {
    StatusLabel.Text = "? 发生错误";
                StatusLabel.Foreground = System.Windows.Media.Brushes.Red;
                MessageBox.Show($"发生错误:\n{ex.Message}\n\n{ex.StackTrace}",
          "错误",
       MessageBoxButton.OK,
           MessageBoxImage.Error);
      }
            finally
            {
             _isStreaming = false;
     StartStreamingButton.IsEnabled = true;
     StopStreamingButton.IsEnabled = false;
          SpeedSlider.IsEnabled = true;
      _stopwatch?.Stop();
      }
        }

        private async Task StreamContent(string content, int delayMs, CancellationToken cancellationToken)
        {
      var sb = new StringBuilder();
       var totalLength = content.Length;

for (int i = 0; i < content.Length; i++)
  {
          if (cancellationToken.IsCancellationRequested)
             {
             StatusLabel.Text = "? 已停止";
      StatusLabel.Foreground = System.Windows.Media.Brushes.Gray;
      break;
     }

          sb.Append(content[i]);

 // 每次添加字符后更新UI（这会触发LaTeX解析，包括不完整的表达式）
            MarkdownViewer.Markdown = sb.ToString();

             // 更新统计信息
       var progress = (i + 1) * 100.0 / totalLength;
        ProgressBar.Value = progress;
            ProgressLabel.Text = $"{progress:F1}%";
    CharCountLabel.Text = $"{i + 1} / {totalLength}";
      TimeElapsedLabel.Text = $"{_stopwatch.Elapsed.TotalSeconds:F1} 秒";

// 自动滚动到底部
          ScrollViewer.ScrollToEnd();

      // 模拟网络延迟
         await Task.Delay(delayMs, cancellationToken);
            }
      }

        private void StopStreaming_Click(object sender, RoutedEventArgs e)
  {
     _cancellationTokenSource?.Cancel();
    }

        private void Clear_Click(object sender, RoutedEventArgs e)
 {
       if (_isStreaming)
            {
                MessageBox.Show("请先停止流式传输", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
      return;
      }

          MarkdownViewer.Markdown = string.Empty;
       ProgressBar.Value = 0;
 ProgressLabel.Text = "0%";
         CharCountLabel.Text = "0";
          TimeElapsedLabel.Text = "0 秒";
    StatusLabel.Text = string.Empty;
        }
    }
}
