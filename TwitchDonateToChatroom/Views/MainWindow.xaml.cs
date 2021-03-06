﻿using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Documents;
using TwitchDonateToChatroom.Models;
using TwitchDonateToChatroom.Service;
using TwitchDonateToChatroom.ViewModels;

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
                OpayCheckService opay = new OpayCheckService(opayid.Text, username.Text, twitchoauth.Text, channelname.Text, messagetemplate.Text);
                state.Text = "Twitch聊天室已連接...\n";
                System.Timers.Timer timer = new System.Timers.Timer(5000)
                {
                    AutoReset = true,
                    Enabled = true
                };
                //timer.Elapsed += await opay.Timer_ElapsedAsync;
                state.Text += "歐付寶頁面擷取中...";
            }
            catch (Exception  ex)
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
            try
            {
                DataConfig data = new DataConfig()
                {
                    ChannelID = channelname.Text,
                    MessageTemplate = messagetemplate.Text,
                    OpayID = opayid.Text,
                    TwitchID = username.Text,
                    TwitchOauth = twitchoauth.Text
                };
                FileStream fs = new FileStream("Config", FileMode.Create, FileAccess.Write);
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, data);
                fs.Close();
                fs.Dispose();
                MessageBox.Show("存檔成功!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }   
        }

        private DataConfig DeserializeBinary()
        {
            if (File.Exists("Config"))
            {
                FileStream fileStream = new FileStream("Config", FileMode.Open);
                BinaryFormatter formatter = new BinaryFormatter();
                DataConfig data = (DataConfig)formatter.Deserialize(fileStream);
                fileStream.Close();
                fileStream.Dispose();
                return data;
            }
            else
                return new DataConfig(); 
        }

        protected override void OnClosed(EventArgs e)
        {
            MainWindowVM vm = DataContext as MainWindowVM;

            vm.CTS.Cancel();

            base.OnClosed(e);
        }
    }
}
