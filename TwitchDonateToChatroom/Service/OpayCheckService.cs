using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using TwitchDonateToChatroom.Models;
using TwitchDonateToChatroom.Service.Interface;

namespace TwitchDonateToChatroom.Service
{
    public class OpayCheckService : IOpayCheckService
    {
        #region Constants

        private const string url = "https://payment.opay.tw/Broadcaster/CheckDonate/";

        #endregion

        #region Fields

        private readonly HttpClient httpClient = new HttpClient();

        private readonly TwitchIRCService irc;

        private readonly string id;

        private int DonatesFlag = 0;

        private readonly string channelName;

        private readonly string messageTemplate;

        #endregion

        #region Constructor

        public OpayCheckService(string opayid, string userName, string oauth, string channelName, string messageTemplate)
        {
            this.id = opayid;

            this.channelName = channelName;

            this.messageTemplate = messageTemplate;

            irc = new TwitchIRCService(userName, oauth, channelName);
        }

        #endregion

        #region Events

        public async Task Timer_ElapsedAsync(object sender, ElapsedEventArgs e)
        {
            try
            {
                var response = await httpClient.PostAsync(url + id, null);

                if (response.IsSuccessStatusCode)
                {
                    var list = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                    var donateList = list.RootElement.GetProperty("lstDonate");

                    if (donateList.EnumerateArray().Count() > 1)
                    {
                        List<Member> donates = JsonSerializer.Deserialize<List<Member>>(donateList.ToString());

                        DonateProcess(donates);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Methods

        private void DonateProcess(List<Member> lists)
        {
            foreach (var item in lists)
            {
                if (DonatesFlag < item.DonateId)
                {
                    irc.Send(channelName, messageTemplate.Replace("{name}", item.Name).Replace("{amount}", item.Amount.ToString()).Replace("{msg}", item.Msg));
                    //Log(item);
                    DonatesFlag = item.DonateId;

                    Thread.Sleep(500);
                }
            }
        }

        [Obsolete]
        private void Log(Member item)
        {
            StreamWriter sw = new StreamWriter(@"log.txt", true);
            string text = $"{DateTime.Now} {item.DonateId} {item.Name} {item.Amount} {item.Msg}";
            sw.WriteLine(text);
            sw.Flush();
            sw.Close();
        }

        #endregion
    }

}
