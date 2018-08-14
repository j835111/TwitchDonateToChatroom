using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

            DataConfig data = DeserializeBinary();
            opayid.Text = data.OpayID;
            channelname.Text = data.ChannelID;
            username.Text = data.TwitchID;
            twitchoauth.Text = data.TwitchOauth;
            messagetemplate.Text = data.MessageTemplate;
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
                MessageBox.Show("OAuth沒填!!");
                return;
            }          
            try
            {
                OpayCheck opay = new OpayCheck(opayid.Text, username.Text, twitchoauth.Text, channelname.Text, messagetemplate.Text);
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("Config", FileMode.Create, FileAccess.Write);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, columname);
            fs.Close();
            fs.Dispose();
            columname.Dispose();
        }

        private DataConfig DeserializeBinary()
        {
            FileStream fileStream = new FileStream("Config", FileMode.Open);
            BinaryFormatter formatter = new BinaryFormatter();
            DataConfig data = (DataConfig)formatter.Deserialize(fileStream);
            fileStream.Close();
            fileStream.Dispose();
            return data;
        }
    }
}
