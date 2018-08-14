using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Documents;

namespace TwitchDonateToIRC
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //public static string 
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (opayid.Text.Length < 1)
            {
                MessageBox.Show("歐付寶 ID 沒填!!");
                return;
            }
            else if (channelname.Text.Length < 1)
            {
                MessageBox.Show("頻道 ID 沒填!!");
                return;
            }      
            else if (username.Text.Length < 1)
            {
                MessageBox.Show("Twitch ID 沒填!!");
                return;
            }
            else if (twitchoauth.Text.Length < 1)
            {
                MessageBox.Show("OAth沒填!!");
                return;
            }          
            try
            {
                OpayCheck opay = new OpayCheck(opayid.Text, username.Text, twitchoauth.Text, channelname.Text);
                state.Text = "Twitch聊天室已連接...\n";
                Timer timer = new Timer(5000)
                {
                    AutoReset = true,
                    Enabled = true
                };
                timer.Elapsed += opay.Timer_Elapsed;
                state.Text += "歐付寶頁面擷取中...";
        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
}

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink source = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(source.NavigateUri.AbsoluteUri));
            e.Handled = true;
        }
    }
}
