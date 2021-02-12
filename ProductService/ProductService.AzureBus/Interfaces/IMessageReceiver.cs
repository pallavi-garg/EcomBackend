namespace ProductService.AzureBus
{
    public interface IMessageReceiver
    {
        void StartReceivingOrdersMadeRequest(int threads);
    }
}
