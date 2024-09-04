using System;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Interfaces.Repositories;

public interface IDishIngredientRepository : IGenericRepository<DishIngredient>
{
Task <List<DishIngredient>> GetByDishIdAsync(int id);
Task<DishIngredient> GetDishIngredientAsync(int dishId, int ingredientId);
}
