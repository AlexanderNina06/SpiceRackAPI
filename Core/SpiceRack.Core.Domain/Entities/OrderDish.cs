using System;

namespace SpiceRack.Core.Domain.Entities;

public class OrderDish
{
public int DishId { get; set; }
public Dish Dish { get; set; }
public int OrderId { get; set; }
public Order Order { get; set; }

}
