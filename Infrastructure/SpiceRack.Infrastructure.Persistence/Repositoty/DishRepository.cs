using System;
using Microsoft.EntityFrameworkCore;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Infrastructure.Persistence.Contexts;

namespace SpiceRack.Infrastructure.Persistence.Repositoty;

public class DishRepository : GenericRepository<Dish>, IDishRepository
{
  private readonly ApplicationContext _db;
    public DishRepository(ApplicationContext context) : base(context)
    {
      _db = context;
    }

    public async Task<List<Dish>> GetAllDishesWhithIngredientsAsync()
    {
       return await _db.Dishes
                    .Include(d => d.DishIngredients)
                    .ThenInclude(i => i.Ingredient)
                    .ToListAsync();
    }

    public async Task<Dish> GetAllDishesWhithIngredientsByIdAsync(int id)
    {
        return await _db.Dishes
                    .Include(d => d.DishIngredients)
                    .ThenInclude(i => i.Ingredient)
                    .FirstOrDefaultAsync(d => d.Id == id);
    }

}
