using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Timers;
using System.Windows;
using Newtonsoft.Json;

namespace TwitchDonateToIRC
{
    class OpayCheck
    {
        HttpWebRequest request;
        TwitchIRC irc;
        private string url = "https://payment.opay.tw/Broadcaster/CheckDonate/";
        private string id;
        private int DonatesFlag { get; set; } = 0;
        private string channelname;

        public string Content { get; set; }
        public OpayCheck(string opayid, string username, string oauth, string channelname)
        {
            this.id = opayid;
            this.channelname = channelname;
            irc = new TwitchIRC(username, oauth, channelname);
        }

        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url + id);
                request.Accept = "application/json";
                request.ContentType = "application/json; charset=utf-8";
                request.ContentLength = 0;
                request.Method = "POST";

                using (StreamReader sr = new StreamReader(request.GetResponse().GetResponseStream()))
                {
                    string text = sr.ReadToEnd();
                    if (text.Length > 10)
                    {
                        List<Member> donates = JsonConvert.DeserializeObject<List<Member>>(text);
                        DonateProcess(donates);
                    }
                    sr.Close();
                }
        }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
}

        public void DonateProcess(List<Member> lists)
        {
            foreach(var item in lists)
            {
                if (DonatesFlag < item.donateid)
                {
                    irc.client.SendMessage(channelname, "/me 贊助者姓名: " + item.name + " 訊息: " + item.msg);
                    DonatesFlag = item.donateid;
                }         
            }
        }
    }
    class Member
    {
        public int donateid { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public string msg { get; set; }
    }
}
