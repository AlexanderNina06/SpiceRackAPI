using System;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Interfaces.Repositories;

public interface IOrderRepository : IGenericRepository<Order>
{
Task<List<Order>> GetAllOrdersWhithDishesAsync();
Task<Order> GetAllOrdersWhithDishesByIdAsync(int id);
Task<List<Order>> GetOrdersByTableId(int id);
}
