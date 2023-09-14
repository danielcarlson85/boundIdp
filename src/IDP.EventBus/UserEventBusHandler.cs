using Azure.Messaging.ServiceBus;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

// This class can later be moved in to another external nuget.


namespace Bound.EventBus
{
    public class UserEventBusHandler : IUserEventBusHandler
    {
        private static ServiceBusClient _serviceBusClient;
        private readonly ServiceBusSender _sender;
        private readonly ServiceBusProcessor _processor;

        public UserEventBusHandler(string connectionString, string queueName, Func<ProcessMessageEventArgs, Task> messageEventFunction, Func<ProcessErrorEventArgs, Task> errorEventFunction)
        {
            _serviceBusClient = new ServiceBusClient(connectionString);
            _sender = _serviceBusClient.CreateSender(queueName);
            _processor = _serviceBusClient.CreateProcessor(queueName, new ServiceBusProcessorOptions());
            _processor.ProcessMessageAsync += messageEventFunction;
            _processor.ProcessErrorAsync += errorEventFunction;
        }

        public async Task StartRecieveMessageAsync()
        {
            await _processor.StartProcessingAsync();
        }

        public async Task SendMessageAsync(string payload)
        {
            Debug.WriteLine($"Message {payload} is sent to User queue");
            ServiceBusMessage message = new(payload);
            await _sender.SendMessageAsync(message);
        }
    }
}
