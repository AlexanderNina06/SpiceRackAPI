using System;
using Microsoft.EntityFrameworkCore;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Infrastructure.Persistence.Contexts;

namespace SpiceRack.Infrastructure.Persistence.Repositoty;

public class DishIngredientRepository : GenericRepository<DishIngredient>, IDishIngredientRepository
{
    private readonly ApplicationContext _db;
    public DishIngredientRepository(ApplicationContext context) : base(context)
    {
        _db = context;
    }

    public async Task<List<DishIngredient>> GetByDishIdAsync(int id)
    {
        return await _db.Set<DishIngredient>().Where(di => di.DishId == id)
        .Include(dish => dish.Ingredient)
        .Include(ingredient => ingredient.Ingredient)
        .ToListAsync(); 
    }

    public async Task<DishIngredient> GetDishIngredientAsync(int dishId, int ingredientId)
    {
        return await _db.Set<DishIngredient>()
        .Where(di => di.DishId == dishId && di.IngredientId == ingredientId)
        .SingleOrDefaultAsync();
    }
}
