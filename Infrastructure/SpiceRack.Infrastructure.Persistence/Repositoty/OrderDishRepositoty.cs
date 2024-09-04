using System;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Infrastructure.Persistence.Contexts;

namespace SpiceRack.Infrastructure.Persistence.Repositoty;

public class OrderDishRepositoty : GenericRepository<OrderDish>, IOrderDishRepositoty
{
  private readonly ApplicationContext _db;
    public OrderDishRepositoty(ApplicationContext context) : base(context)
    {
      _db = context;
    }

    public async Task<List<OrderDish>> GetByOrderIdAsync(int id)
    {
        return await _db.Set<OrderDish>().Where(order => order.OrderId == id)
        .Include(dish => dish.Dish)
        .ToListAsync();
    }

    public async Task<OrderDish> GetOrderDishesAsync(int orderId, int dishId)
    {
        return await _db.Set<OrderDish>()
        .Where(or => or.OrderId == orderId && or.DishId == dishId)
        .SingleOrDefaultAsync();
    }
}
