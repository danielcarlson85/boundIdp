using System.Threading.Tasks;

namespace Bound.EventBus
{
    public interface IUserEventBusHandler
    {
        Task SendMessageAsync(string payload);
        Task StartRecieveMessageAsync();
    }
}