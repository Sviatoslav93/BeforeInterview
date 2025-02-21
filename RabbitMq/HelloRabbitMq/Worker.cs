using Microsoft.Extensions.Hosting;

namespace HelloRabbitMq;

internal class Worker(RabbitMqService rabbitMqService) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine("Sending message...");
            await rabbitMqService.SendMessage("Hello RabbitMq", stoppingToken);
            await Task.Delay(1000, stoppingToken);
        }
    }
}