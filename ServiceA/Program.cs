using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ServiceA;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/api/service-a/hello", () => "Hello from service A");

app.MapGet("/api/service-a/world", async () =>
{
    return "WORLD";
});

app.Run();