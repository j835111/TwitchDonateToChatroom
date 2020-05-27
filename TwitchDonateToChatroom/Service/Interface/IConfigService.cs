using System.Threading.Tasks;
using TwitchDonateToChatroom.Models;

namespace TwitchDonateToChatroom.Service.Interface
{
    public interface IConfigService
    {
        Task<DataConfig> LoadAsync();

        Task SaveAsync(DataConfig config);
    }
}