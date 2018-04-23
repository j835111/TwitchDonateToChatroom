using System.Threading;
using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace TwitchDonateToIRC
{
    class TwitchIRC
    {
        public TwitchClient client;
        public TwitchIRC(string username, string oauth, string channelname)
        {
            client = new TwitchClient();
            ConnectionCredentials credentials = new ConnectionCredentials(username, oauth);
            client.Initialize(credentials, channelname);
            client.Connect();
            Thread.Sleep(1000);
        } 
    }
}
