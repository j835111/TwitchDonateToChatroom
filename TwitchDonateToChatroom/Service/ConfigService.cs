using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using TwitchDonateToChatroom.Models;
using TwitchDonateToChatroom.Service.Interface;

namespace TwitchDonateToChatroom.Service
{
    public class ConfigService : IConfigService
    {
        private const string fileName = "Config";

        public async Task<DataConfig> LoadAsync()
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                string text = await sr.ReadToEndAsync();

                return JsonSerializer.Deserialize<DataConfig>(text);
            }
        }

        public async Task SaveAsync(DataConfig config)
        {
            if (File.Exists("Config"))
                File.Delete("Config");

            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                await sw.WriteAsync(JsonSerializer.Serialize(config));
            }
        }
    }
}
