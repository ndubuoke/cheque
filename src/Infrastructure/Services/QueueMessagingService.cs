using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ChequeMicroservice.Application.Common.Interfaces;
using Serilog;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChequeMicroservice.Infrastructure.Services;

public class QueueMessagingService : IQueueMessagingService
{
    private readonly ServiceBusClient _client;
    private readonly IConfiguration _configuration;

    public QueueMessagingService(IConfiguration configuration)
    {
        _configuration = configuration;
        _client = new ServiceBusClient(_configuration["AzureServiceBus:ConnectionString"]);
    }

    public async Task PublishTopicMessage(dynamic message, string subject)
    {
        try
        {
            message.Id = Guid.NewGuid();
            var jsonMessage = JsonConvert.SerializeObject(message);
            var busMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
            {
                PartitionKey = Guid.NewGuid().ToString(),
                Label = subject
            };
            ISenderClient topicClient = new TopicClient(_configuration["AzureServiceBus:ConnectionString"], _configuration["AzureServiceBus:TopicName"]);
            await topicClient.SendAsync(busMessage);
            Console.WriteLine($"Sent message to {topicClient.Path}");
            await topicClient.CloseAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Log.Error(ex.ToString());
        }
    }
    public async Task PublishReminderMessage(dynamic message, string subject)
    {
        try
        {
            message.Id = Guid.NewGuid();
            var jsonMessage = JsonConvert.SerializeObject(message);
            var busMessage = new Message(Encoding.UTF8.GetBytes(jsonMessage))
            {
                PartitionKey = Guid.NewGuid().ToString(),
                Label = subject
            };
            ISenderClient topicClient = new TopicClient(_configuration["AzureServiceBus:ConnectionString"], _configuration["AzureServiceBus:ReminderTopicName"]);
            await topicClient.SendAsync(busMessage);
            Console.WriteLine($"Sent message to {topicClient.Path}");
            await topicClient.CloseAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Log.Error(ex.ToString());
        }
    }
    public ServiceBusProcessor ConsumeMessage(string topicName, string subscriptionName)
    {

        return _client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions
        {
            // Set options for processing
            MaxConcurrentCalls = 16, // Adjust based on your scenario
        });
    }

    public async Task PublishToQueue(dynamic msg, string queueName)
    {
        QueueClient queueClient = new QueueClient(_configuration["AzureServiceBus:ConnectionString"], queueName);

        try
        {
            var jsonMessage = JsonConvert.SerializeObject(msg);
            Message message = new Message(Encoding.UTF8.GetBytes(jsonMessage));

            await queueClient.SendAsync(message);

            Console.WriteLine("Message sent to the queue.");
        }
        catch (Exception ex)
        {
            Log.Error(ex.ToString());
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            await queueClient.CloseAsync();
        }
    }
}
