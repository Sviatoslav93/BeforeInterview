
using HelloRabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateApplicationBuilder(args);

host.Configuration
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables();

host.Services.Configure<RabbitMqConfiguration>(host.Configuration.GetSection("Rabbitmq"));

host.Services.AddScoped<RabbitMqService>();
host.Services.AddHostedService<Worker>();


await host.Build().RunAsync();
