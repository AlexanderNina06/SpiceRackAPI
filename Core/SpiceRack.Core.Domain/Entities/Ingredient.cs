using System;
using SpiceRack.Core.Domain.Common;

namespace SpiceRack.Core.Domain.Entities;

public class Ingredient : AuditableBaseEntity
{
public string Name { get; set; }
public ICollection<DishIngredient> DishIngredients { get; set; }

}
