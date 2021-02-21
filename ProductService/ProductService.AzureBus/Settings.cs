namespace ProductService.AzureBus
{
    public class Settings
    {
        //ToDo: Enter a valid Service Bus connection string
        public static string ConnectionString = "Endpoint=sb://amcartneworder.servicebus.windows.net/;SharedAccessKeyName=orderplacedpolicy;SharedAccessKey=VDe1roKZJDWHIiVMreebZP8WIGuJ0p8zLAokTg2zQfY=";
        public static string QueueName = "orderplaced";
    }
}
