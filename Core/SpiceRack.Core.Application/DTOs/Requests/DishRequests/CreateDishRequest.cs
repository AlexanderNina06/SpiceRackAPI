using System;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.DishRequests;

public class CreateDishRequest
{

[SwaggerParameter(Description = "Name of the dish")]
public string Name { get; set; }

[SwaggerParameter(Description = "Price of the dish")]
public double Price { get; set; }

[SwaggerParameter(Description = "Number of servings of the dish")]
public int Servings { get; set; }

/// <example>(Just One of these: Appetizer, MainCourse, Dessert or Drink)</example>
[SwaggerParameter(Description = "Category of the dish (Appetizer, MainCourse, Dessert, Drink)")]
public string Category { get; set; }

/// <example>
/// [1, 2]
/// </example>
[SwaggerParameter(Description = "List of ingredient IDs(number) that compose the dish")]
public ICollection<int> DishIngredients { get; set; }
}
