using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductService.AzureBus
{
    public class MessageReceiver : IMessageReceiver
    {
        private QueueClient QueueClient;

        IProductInventoryManager _productInventoryManager;

        public MessageReceiver(IProductInventoryManager productInventoryManager)
        {
            _productInventoryManager = productInventoryManager;
        }

        public void StartReceivingOrdersMadeRequest(int threads)
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

        public async Task StopReceivingOrdersMadeRequest()
        {
            // Close the client, which will stop the message pump.
            await QueueClient.CloseAsync();
        }

        private async Task ProcessOrdersMadeMessageAsync(Message message, CancellationToken token)
        {

            // Deserialize the message body.
            var messageBodyText = Encoding.UTF8.GetString(message.Body);

            var order = JsonConvert.DeserializeObject<ProductOrder>(messageBodyText);

            // Process the message
            UpdateProductOrder(order);

            // Complete the message
            await QueueClient.CompleteAsync(message.SystemProperties.LockToken);

        }

        private void UpdateProductOrder(ProductOrder order)
        {
            _productInventoryManager?.UpdateProductQuantity(order);
        }

        private Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            //Add log
            return Task.CompletedTask;
        }
    }
}
