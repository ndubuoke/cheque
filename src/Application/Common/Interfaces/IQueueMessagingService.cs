using Azure.Messaging.ServiceBus;
using System.Threading.Tasks;

namespace ChequeMicroservice.Application.Common.Interfaces;

public interface IQueueMessagingService
{
    Task PublishTopicMessage(dynamic message, string subject);
    Task PublishReminderMessage(dynamic message, string subject);
    ServiceBusProcessor ConsumeMessage(string topicName, string subscriptionName);
    Task PublishToQueue(dynamic msg, string queueName);
}
