using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDonateToIRC
{
    [Serializable]
    struct DataConfig
    {
        public string OpayID { get; set; }
        public string ChannelID { get; set; }
        public string TwitchID { get; set; }
        public string TwitchOauth { get; set; }
        public string MessageTemplate { get; set; }
    }
}
