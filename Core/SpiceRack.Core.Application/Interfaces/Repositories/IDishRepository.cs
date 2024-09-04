using System;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Core.Application.Interfaces.Repositories;

public interface IDishRepository : IGenericRepository<Dish>
{
Task<List<Dish>> GetAllDishesWhithIngredientsAsync();
Task<Dish> GetAllDishesWhithIngredientsByIdAsync(int id);

}
