using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var host = Host.CreateApplicationBuilder(args);

host.Configuration
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();

host.Services.Configure<RabbitMqConfiguration>(host.Configuration.GetSection("Rabbitmq"));

host.Services.AddHostedService<MyBackgroundService>();

await host.Build().RunAsync();

internal class MyBackgroundService(
    ILogger<MyBackgroundService> logger,
    IOptions<RabbitMqConfiguration> rabbitMqconfig) : BackgroundService
{
    private readonly ILogger<MyBackgroundService> _logger = logger;
    private readonly RabbitMqConfiguration _rabbitMqconfig = rabbitMqconfig.Value;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        var connectionFactory = new ConnectionFactory
        {
            HostName = _rabbitMqconfig.HostName,
            UserName = _rabbitMqconfig.UserName,
            Password = _rabbitMqconfig.Password,
        };
        await using var connection = await connectionFactory.CreateConnectionAsync(ct);
        await using var channel = await connection.CreateChannelAsync(cancellationToken: ct);

        await channel.QueueDeclareAsync(queue: _rabbitMqconfig.QueueName, durable: false, exclusive: false, autoDelete: false, arguments: null, cancellationToken: ct);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation("Received message: {Message}", message);
            return Task.CompletedTask;
        };

        await channel.BasicConsumeAsync(_rabbitMqconfig.QueueName, autoAck: true, consumer: consumer, cancellationToken: ct);

        while (!ct.IsCancellationRequested)
        {
            await Task.Delay(2000, ct);
        }

        _logger.LogInformation("Background service is stopping.");
        await connection.CloseAsync(cancellationToken: ct);
    }
}
