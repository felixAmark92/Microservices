using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceA;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<RabbitMqConnection>();
builder.Services.AddScoped<RabbitMqProducer>();

var app = builder.Build();
var index = File.ReadAllText("index.html");

app.MapGet("/index", ()=> Results.Content(index, "text/html"));

app.MapGet("/api/service-a/hello", () => "Hello from service A");

app.MapGet("/api/service-a/hello-to-b", async (RabbitMqProducer producer) =>
{
    var test = new InventoryDto()
    {
        ProductId = 1,
        Quantity = 10,
    };
    await producer.SendMessage(test);
    return "Successfully sent log to service B";
});

app.Run();