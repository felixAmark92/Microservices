using ApiService.Orders;
using ApiService.Orders.Dtos;
using ApiService.Orders.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IRabbitMqContext, RabbitMqContext>();
builder.Services.AddScoped<IMqProducer, MqProducer>();

builder.Services.AddScoped<IOrderService<OrderDto>, OrderService>();
//builder.Services.AddScoped<IOrderRepository<OrderEntity>, OrderRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/orders", async (IOrderService<OrderDto> orderService) =>
{
    await orderService.GetAllAsync();
    return Results.Ok("messageReceived : " + DateTime.Now);
});

app.MapPost("/api/orders", async (OrderDto orderDto, IOrderService<OrderDto> orderService) =>
{
    await orderService.AddAsync(orderDto);
    return Results.Ok();
});

app.Run();

