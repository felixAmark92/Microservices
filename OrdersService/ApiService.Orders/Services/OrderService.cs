using ApiService.Orders.Dtos;

namespace ApiService.Orders.Services;

interface IOrderService<T> where T : class
{
    Task AddAsync(T dto);
    Task UpdateAsync(T dto);
    Task DeleteAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetOrderByIdAsync(string id);
}

public class OrderService(MqProducer mqProducer): IOrderService<OrderDto>
{
    public async Task AddAsync(OrderDto dto)
    {
        await mqProducer.PublishMessage(dto.ToString(), "orders.create");
    }

    public async Task UpdateAsync(OrderDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<OrderDto> GetOrderByIdAsync(string id)
    {
        throw new NotImplementedException();
    }
}