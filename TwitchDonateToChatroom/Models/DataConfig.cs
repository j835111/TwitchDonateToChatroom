namespace TwitchDonateToChatroom.Models
{
    public class DataConfig
    {
        public string OpayId { get; set; }

        public string ChannelName { get; set; }

        public string UserName { get; set; }

        public string TwitchOauth { get; set; }

        public string MessageTemplate { get; set; } = "/me 姓名:{name} 金額:{amount} 訊息:{msg}";
    }
}
