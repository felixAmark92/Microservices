using ApiService.Orders.Dtos;
using ApiService.Orders.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<IOrderService<OrderDto>, OrderService>();
//builder.Services.AddScoped<IOrderRepository<OrderEntity>, OrderRepository>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/api/orders", async (IOrderService<OrderDto> orderService) =>
{
    return Results.Ok("messageReceived : " + DateTime.Now);
    //return Results.Ok(await orderService.GetAllAsync());
});

app.MapPost("/api/orders", async (OrderDto orderDto, IOrderService<OrderDto> orderService) =>
{
    await orderService.AddAsync(orderDto);
    return Results.Ok();
});

app.Run();

