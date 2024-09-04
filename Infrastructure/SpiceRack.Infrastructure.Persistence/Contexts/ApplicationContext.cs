using System;
using Microsoft.EntityFrameworkCore;
using SpiceRack.Core.Domain.Common;
using SpiceRack.Core.Domain.Entities;

namespace SpiceRack.Infrastructure.Persistence.Contexts;

public class ApplicationContext : DbContext 
{
public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){}

public DbSet<Dish> Dishes { get; set; }
public DbSet<Ingredient> Ingredients { get; set; }
public DbSet<Order> Orders{ get; set; }
public DbSet<Table> Tables{ get; set; }
public DbSet<DishIngredient> DishIngredient{ get; set; }
public DbSet<OrderDish> OrderDishes{ get; set; }



public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
{
    foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
    {
        switch (entry.State)
        {
            case EntityState.Added:
                entry.Entity.Created = DateTime.Now;
                entry.Entity.CreatedBy = "DefaultAppUser";
                break;

            case EntityState.Modified:
                entry.Entity.LastModified = DateTime.Now;
                entry.Entity.LastModifiedBy = "DefaultAppUser";
                break;
        }
    }
    return base.SaveChangesAsync(cancellationToken);
}

protected override void OnModelCreating(ModelBuilder modelBuilder)
{
  base.OnModelCreating(modelBuilder);  

  #region tables 

   modelBuilder.Entity<Dish>()
   .ToTable("Dishes");  

   modelBuilder.Entity<Order>()
   .ToTable("Orders");  

   modelBuilder.Entity<Table>()
   .ToTable("Tables");  

   modelBuilder.Entity<Ingredient>()
   .ToTable("Ingredients");     
  
  #endregion
    
  #region Primary Keys

   modelBuilder.Entity<Dish>()
   .HasKey(d => d.Id);  

   modelBuilder.Entity<Ingredient>()
   .HasKey(I => I.Id);  

   modelBuilder.Entity<Order>()
   .HasKey(O => O.Id);  

   modelBuilder.Entity<Table>()
   .HasKey(T => T.Id);  

   modelBuilder.Entity<DishIngredient>()
    .HasKey(di => new { di.DishId, di.IngredientId });

   modelBuilder.Entity<OrderDish>()
    .HasKey(di => new { di.DishId, di.OrderId });
      

  #endregion
    
  #region Relationships

  modelBuilder.Entity<Order>()
  .HasOne<Table>(t => t.Table)
  .WithMany()
  .HasForeignKey(t => t.TableId)
  .OnDelete(DeleteBehavior.Cascade);    

  modelBuilder.Entity<DishIngredient>()
        .HasOne(di => di.Dish)
        .WithMany(d => d.DishIngredients)
        .HasForeignKey(di => di.DishId);

    modelBuilder.Entity<DishIngredient>()
        .HasOne(di => di.Ingredient)
        .WithMany(i => i.DishIngredients)
        .HasForeignKey(di => di.IngredientId);  
    
    modelBuilder.Entity<OrderDish>()
        .HasOne(di => di.Order)
        .WithMany(i => i.OrderDishes)
        .HasForeignKey(di => di.OrderId);  
    
    modelBuilder.Entity<OrderDish>()
        .HasOne(di => di.Dish)
        .WithMany(i => i.OrderDishes)
        .HasForeignKey(di => di.DishId);

  #endregion
}
}
