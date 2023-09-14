using System.Threading.Tasks;

namespace Bound.EventBus
{
    public interface ITestEventBusHandler
    {
        Task SendMessageAsync(string payload);
        Task StartRecieveMessageAsync();
    }
}