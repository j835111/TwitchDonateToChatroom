using TwitchLib.Client;
using TwitchLib.Client.Models;

namespace TwitchDonateToChatroom.Service
{
    public class TwitchIRCService
    {
        private readonly TwitchClient client = new TwitchClient();

        public TwitchIRCService(string username, string oauth, string channelname)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(username, oauth);

            client.Initialize(credentials, channelname);

            client.Connect();
        }

        public void Send(string channelName,string message)
        {
            client.SendMessage(channelName, message);
        }
    }
}
