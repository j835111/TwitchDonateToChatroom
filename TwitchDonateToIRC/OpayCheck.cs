using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Timers;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                    JObject list = (JObject)JsonConvert.DeserializeObject(text);
                    if (list["lstDonate"].ToString().Length > 10)
                    {
                        List<Member> donates = JsonConvert.DeserializeObject<List<Member>>(list["lstDonate"].ToString());
                        DonateProcess(donates);
                    }
                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void DonateProcess(List<Member> lists)
        {
            foreach (var item in lists)
            {
                if (DonatesFlag < item.donateid)
                {
                    irc.client.SendMessage(channelname, $"/me {item.name} 丟了{item.amount}元到許願池向阿米女神許願: {item.msg}");
                    //Log(item);
                    DonatesFlag = item.donateid;
                    Thread.Sleep(500);
                }
            }
        }

        private void Log(Member item)
        {
            StreamWriter sw = new StreamWriter(@"log.txt", true);
            string text = $"{DateTime.Now} {item.donateid} {item.name} {item.amount} {item.msg}";
            sw.WriteLine(text);
            sw.Flush();
            sw.Close();
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
