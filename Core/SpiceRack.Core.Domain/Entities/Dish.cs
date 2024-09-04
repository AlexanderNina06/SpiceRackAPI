using System;
using System.Runtime.CompilerServices;
using SpiceRack.Core.Domain.Common;
using SpiceRack.Core.Domain.Enums;

namespace SpiceRack.Core.Domain.Entities;

public class Dish : AuditableBaseEntity
{
public string Name { get; set; }
public double Price { get; set; }
public int Servings { get; set; }
public DishTypes Category { get; set; }
public ICollection<DishIngredient> DishIngredients { get; set; }
public ICollection<OrderDish> OrderDishes { get; set; }

}
