using Bound.EventBus;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Bound.IDP.Runtime
{
    public static class EventbusExtensionMethods
    {
        public static void AddUserEventBus(this IServiceCollection services, string eventBusConnectionString)
        {
            // TO use another service when a message is recieved get DI for that service:
            //var userService = services.BuildServiceProvider().GetService<UserService>();

            Debug.WriteLine("Starting listening on user queue");

            services.AddTransient<IUserEventBusHandler, UserEventBusHandler>(_ => new UserEventBusHandler(eventBusConnectionString, "usercreatedqueue",
                async message =>
                {
                    Debug.WriteLine($"Message recieved: {message.Message.Body} from User queue");
                    await message.CompleteMessageAsync(message.Message);
                },
                 error =>
                 {
                     Debug.WriteLine(error.Exception.ToString());
                     return Task.CompletedTask;
                 })
            );
        }

        public static void AddTestEventBus(this IServiceCollection services, string eventBusConnectionString)
        {
            Debug.WriteLine("Starting listening on test queue");

            services.AddTransient<ITestEventBusHandler, TestEventBusHandler>(_ => new TestEventBusHandler(eventBusConnectionString, "test",
                async message =>
                {
                    Debug.WriteLine($"Message recieved: {message.Message.Body} from test queue");
                    await message.CompleteMessageAsync(message.Message);
                },
                error =>
                {
                    Debug.WriteLine(error.Exception.ToString());
                    return Task.CompletedTask;
                })
            );
        }
    }
}