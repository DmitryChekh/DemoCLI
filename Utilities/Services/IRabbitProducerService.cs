namespace Utilities.Services
{
    public interface IRabbitProducerService
    {
        void Send(string message);
    }
}