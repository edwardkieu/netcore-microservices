using System.Threading.Tasks;

namespace OrderAPI.AzureServiceBusMessaging
{
    public interface IAzureServiceBusConsumer
    {
        Task Start();
        Task Stop();
    }
}
