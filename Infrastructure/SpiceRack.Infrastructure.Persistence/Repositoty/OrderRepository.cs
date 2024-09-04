using System;
using Microsoft.EntityFrameworkCore;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Core.Domain.Enums;
using SpiceRack.Infrastructure.Persistence.Contexts;

namespace SpiceRack.Infrastructure.Persistence.Repositoty;

public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
  private readonly ApplicationContext _db;
    public OrderRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }

    public async Task<List<Order>> GetAllOrdersWhithDishesAsync()
    {
        return await _db.Orders
        .Include(o => o.OrderDishes)
        .ThenInclude(d => d.Dish)
        .ToListAsync();
    }

    public async Task<Order> GetAllOrdersWhithDishesByIdAsync(int id)
    {
        return await _db.Orders
        .Include(o => o.OrderDishes)
        .ThenInclude(d => d.Dish)
        .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Order>> GetOrdersByTableId(int id)
    {
        return await _db.Set<Order>()
        .Where(o => o.TableId == id && o.Status == OrderStatus.InProgress)
        .Include(o => o.Table)
        .Include(o => o.OrderDishes)
        .ThenInclude(d => d.Dish)
        .ToListAsync();
    }
}
