using System;
using System.Collections.Generic;
using System.Text;
using TwitchDonateToChatroom.Service.Interface;

namespace TwitchDonateToChatroom.ViewModels
{
    public class MainWindowVM : ViewModelBase
    {
        #region Fields

        private readonly IOpayCheckService _opayCheckService;

        private string opayId = null;

        private string channelName = null;



        #endregion

        #region Constructor

        public MainWindowVM(IOpayCheckService opayCheckService)
        {
            this._opayCheckService = opayCheckService;
        }

        #endregion

        #region Properties

        public string OpayId
        {
            get => opayId;
            set
            {
                SetProperty(ref opayId, value, nameof(OpayId));
            }
        }

        public string ChannelName { get; set; }

        public string UserName { get; set; }

        public string Oauth { get; set; }

        public string MessageTamplate { get; set; }


    }
}
