using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.AzureBus
{
    public class MessageSender
    {
        public static async Task SendOrderPlacedAsync(List<ProductOrder> orderDetails)
        {
            var client = new QueueClient(Settings.ConnectionString, Settings.QueueName);

            var messageList = new List<Message>();

            foreach (var order in orderDetails)
            {
                var orderJson = JsonConvert.SerializeObject(order);
                var message = new Message(Encoding.UTF8.GetBytes(orderJson))
                {
                    Label = "OrderPlaced",
                    ContentType = "application/json"
                };
                messageList.Add(message);
            }

            await client.SendAsync(messageList);
            await client.CloseAsync();
        }
    }
}
