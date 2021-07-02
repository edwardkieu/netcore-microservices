using AzureServiceBus.Models;
using System.Threading.Tasks;

namespace AzureServiceBus.Interfaces
{
    public interface IMessageBusService
    {
        Task PublishMessage(BaseMessage message, string connectionString, string queueOrTopicName);
    }
}
