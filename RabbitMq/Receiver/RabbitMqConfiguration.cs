internal class RabbitMqConfiguration
{
    public string HostName { get; set; } = string.Empty;
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string QueueName { get; set; } = null!;
}
