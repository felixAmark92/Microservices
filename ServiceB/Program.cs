
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/api/service-b", () => "Hello from service B");

var factory = new ConnectionFactory()
{
    HostName = "rabbit-mq"
};
var connection = await factory.CreateConnectionAsync();

await using var channel = await connection.CreateChannelAsync();
await channel.QueueDeclareAsync("standard", exclusive: false, autoDelete: true);
var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine(message);
};
await channel.BasicConsumeAsync(queue: "standard", autoAck: true, consumer: consumer);

app.Run();