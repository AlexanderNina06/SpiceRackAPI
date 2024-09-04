using System;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Interfaces.Repositories;

public interface IOrderDishRepositoty : IGenericRepository<OrderDish>
{
Task <List<OrderDish>> GetByOrderIdAsync(int id);
Task<OrderDish> GetOrderDishesAsync(int orderId, int dishId);

}
