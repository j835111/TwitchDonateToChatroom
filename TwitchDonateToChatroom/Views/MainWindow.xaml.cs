using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace TwitchDonateToChatroom.Views
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Background = new LinearGradientBrush(
                new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString("#474850"), 0),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#17181B"), 1)
                },
            229.01);

            rectangle.Fill= new LinearGradientBrush(
                new GradientStopCollection
                {
                    new GradientStop((Color)ColorConverter.ConvertFromString("#4B4B4B"), 0),
                    new GradientStop((Color)ColorConverter.ConvertFromString("#151515"), 1)
                },
            202.01);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink source = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(source.NavigateUri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
