using System;

namespace SpiceRack.Core.Application.DTOs.Responses.DishResponses;

public class GetDishResponse
{
public int Id { get; set; }
public string Name { get; set; }
public double Price { get; set; }
public int Servings { get; set; }
public string Category { get; set; }
public ICollection<string> DishIngredients { get; set; }
}
