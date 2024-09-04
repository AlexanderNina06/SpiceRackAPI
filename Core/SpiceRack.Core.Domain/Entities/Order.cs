using System;
using SpiceRack.Core.Domain.Common;
using SpiceRack.Core.Domain.Enums;

namespace SpiceRack.Core.Domain.Entities;

public class Order : AuditableBaseEntity
{
public Table Table { get; set; }
public int TableId { get; set; }
public decimal Subtotal { get; set; }
public OrderStatus Status{ get; set; }
public ICollection<OrderDish> OrderDishes { get; set; }

}
