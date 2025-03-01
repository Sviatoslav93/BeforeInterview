using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace Sender;

public class RabbitMqService
{
    private readonly RabbitMqConfiguration _rabbitMqConfiguration;
    private readonly ConnectionFactory _connectionFactory;
    private readonly ILogger<RabbitMqService> _logger;

    public RabbitMqService(ILogger<RabbitMqService> logger, IOptions<RabbitMqConfiguration> rabbitMqConfiguration)
    {
        _logger = logger;
        _rabbitMqConfiguration = rabbitMqConfiguration.Value;
        _connectionFactory = CreateConnectionFactory();
    }

    public async Task SendMessage(string message, CancellationToken cancellationToken = default)
    {
        await using var connection = await _connectionFactory.CreateConnectionAsync(cancellationToken);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: cancellationToken);

        // Declare the queue
        var queueName = _rabbitMqConfiguration.QueueName;
        await DeclareQueueAsync(channel, queueName, cancellationToken);

        // Send the message
        var messageBody = Encoding.UTF8.GetBytes(message);
        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: queueName,
            mandatory: false,
            body: messageBody,
            cancellationToken: cancellationToken
        );

        _logger.LogInformation("Message sent successfully: {Message}", message);
    }

    private ConnectionFactory CreateConnectionFactory()
    {
        return new ConnectionFactory
        {
            HostName = _rabbitMqConfiguration.HostName,
            UserName = _rabbitMqConfiguration.UserName,
            Password = _rabbitMqConfiguration.Password,
        };
    }

    private static async Task DeclareQueueAsync(IChannel channel, string queueName, CancellationToken cancellationToken)
    {
        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null,
            cancellationToken: cancellationToken
        );
    }
}
