using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchDonateToIRC
{
    [Serializable]
    class DataConfig : IDisposable
    {
        bool disposed = false;

        public string OpayID { get; set; }
        public string ChannelID { get; set; }
        public string TwitchID { get; set; }
        public string TwitchOauth { get; set; }
        public string MessageTemplate { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {

            }
            disposed = true;
        }
    }
}
