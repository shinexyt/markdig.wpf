using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Markdig.Wpf.SampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool useExtensions = true;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            LoadDocument("Documents/Markdig-readme.md");
        }

        private void OpenHyperlink(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            Process.Start(e.Parameter.ToString());
        }

        private void ClickOnImage(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            MessageBox.Show($"URL: {e.Parameter}");
        }

        private void ToggleExtensionsButton_OnClick(object sender, RoutedEventArgs e)
        {
            useExtensions = !useExtensions;
            Viewer.Pipeline = useExtensions ? new MarkdownPipelineBuilder().UseSupportedExtensions().Build() : new MarkdownPipelineBuilder().Build();
        }

        private void LoadMathDemoButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoadDocument("Documents/Math-Demo.md");
        }

        private void LoadLatexTestButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoadDocument("Documents/LaTeX-Syntax-Test.md");
        }

        private void LoadTaylorButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoadDocument("Documents/Taylor-Formula-Correct.md");
        }

        private void LoadReadmeButton_OnClick(object sender, RoutedEventArgs e)
        {
            LoadDocument("Documents/Markdig-readme.md");
        }

        private void LoadDocument(string path)
        {
            if (File.Exists(path))
            {
                Viewer.Markdown = File.ReadAllText(path);
            }
            else
            {
                Viewer.Markdown = $"# Error\n\nDocument not found: {path}";
            }
        }
    }
}
