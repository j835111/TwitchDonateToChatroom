using System.Threading.Tasks;
using System.Timers;

namespace TwitchDonateToChatroom.Service.Interface
{
    public interface IOpayCheckService
    {
        Task Timer_ElapsedAsync();
    }
}