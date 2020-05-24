namespace TwitchDonateToChatroom.Models
{
    public class DataConfig
    {
        public string OpayID { get; set; }

        public string ChannelID { get; set; }

        public string TwitchID { get; set; }

        public string TwitchOauth { get; set; }

        public string MessageTemplate { get; set; } = "/me 姓名:{name} 金額:{amount} 訊息:{msg}";
    }
}
