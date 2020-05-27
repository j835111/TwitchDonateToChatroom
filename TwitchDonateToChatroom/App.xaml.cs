using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TwitchDonateToChatroom.Service;
using TwitchDonateToChatroom.Service.Interface;
using TwitchDonateToChatroom.Views;

namespace TwitchDonateToChatroom
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(typeof(MainWindow));
            services.AddSingleton<IOpayCheckService, OpayCheckService>();
            services.AddSingleton<IConfigService, ConfigService>();
        }
    }
}
