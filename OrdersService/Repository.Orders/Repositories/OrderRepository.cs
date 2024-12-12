using Microsoft.EntityFrameworkCore;
using Repository.Orders.Entities;

namespace Repository.Orders.Repositories;

public interface IOrderRepository<T> : IBaseRepository<T> where T : class
{
    
}
public class OrderRepository(OrdersDbContext context) : BaseRepository<OrderEntity>(context), IOrderRepository<OrderEntity>
{
    
  
}