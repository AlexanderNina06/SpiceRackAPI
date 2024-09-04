using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpiceRack.Core.Application.Interfaces.Repositories;
using SpiceRack.Infrastructure.Persistence.Contexts;
using SpiceRack.Infrastructure.Persistence.Repositoty;

namespace SpiceRack.Infrastructure.Persistence;

public static class ServiceRegistration
{
public static void AddPersistenceInfrastructure(this IServiceCollection services,IConfiguration configuration)
{
    #region Contexts
    if (configuration.GetValue<bool>("UseInMemoryDatabase"))
    {
        services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
    }
    else
    {
        services.AddDbContext<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("Default"),
        m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
    }
    #endregion

    #region Repositories
    services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    services.AddTransient<IIngredientRepository, IngredientRepository>();
    services.AddTransient<ITableRepository, TableRepository>();
    services.AddTransient<IDishIngredientRepository, DishIngredientRepository>();
    services.AddTransient<IDishRepository, DishRepository>();
    services.AddTransient<IOrderRepository, OrderRepository>();
    services.AddTransient<IOrderDishRepositoty, OrderDishRepositoty>();

    #endregion
}
}
