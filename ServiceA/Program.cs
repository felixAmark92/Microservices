using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceA;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(new RabbitMqConnection())
    .AddScoped<RabbitMqProducer>();

var app = builder.Build();

app.MapGet("/api/service-a", () => "Hello from service A");

app.MapPost("/api/service-a/to-service-b", async (RabbitMqProducer producer) =>
{
    await producer.SendMessage("Hello from service A");
    return "You have successfully sent a message from service A to service B";
});


app.Run();