using System;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Core.Domain.Entities;
using SpiceRack.Infrastructure.Persistence.Contexts;

namespace SpiceRack.Infrastructure.Persistence.Repositoty;

public class IngredientRepository : GenericRepository<Ingredient>, IIngredientRepository
{
    private readonly ApplicationContext _db;

    public IngredientRepository(ApplicationContext context) : base(context)
    {
        _db = context;
    }
}
