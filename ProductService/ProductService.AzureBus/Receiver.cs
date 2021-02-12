using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.AzureBus
{
    public static class MessageReceiver
    {
        private static QueueClient QueueClient;

        public static void StartReceivingOrdersMadeRequest(int threads)
        {
            // Create a new client
            QueueClient = new QueueClient(Settings.ConnectionString, Settings.QueueName);

            // Set the options for the message handler
            var options = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                AutoComplete = false,
                MaxConcurrentCalls = threads,
                MaxAutoRenewDuration = TimeSpan.FromMinutes(10)
            };

            // Create a message pump using RegisterMessageHandler
            QueueClient.RegisterMessageHandler(ProcessOrdersMadeMessageAsync, options);

        }

        public static async Task StopReceivingOrdersMadeRequest()
        {
            // Close the client, which will stop the message pump.
            await QueueClient.CloseAsync();
        }

        private static async Task ProcessOrdersMadeMessageAsync(Message message, CancellationToken token)
        {

            // Deserialize the message body.
            var messageBodyText = Encoding.UTF8.GetString(message.Body);

            var order = JsonConvert.DeserializeObject<ProductOrder>(messageBodyText);

            // Process the message
            UpdateProductOrder(order);

            // Complete the message
            await QueueClient.CompleteAsync(message.SystemProperties.LockToken);

        }

        private static void UpdateProductOrder(ProductOrder order)
        {

        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            //Add log
            return Task.CompletedTask;
        }
    }
}
