using System;

namespace SpiceRack.Core.Domain.Entities;

public class DishIngredient
{
public int DishId { get; set; }
public Dish Dish { get; set; }
public int IngredientId { get; set; }
public Ingredient Ingredient { get; set; }
}
