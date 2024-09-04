using System;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.DishRequests;

public class UpdateDishRequest
{

[SwaggerParameter(Description = "ID of the dish to update")]
public int Id { get; set; }

[SwaggerParameter(Description = "Optional: New name of the dish")]
public string? Name { get; set; }

[SwaggerParameter(Description = "Optional: New price of the dish")]
public double? Price { get; set; }

[SwaggerParameter(Description = "Optional: New number of servings of the dish")]
public int? Servings { get; set; }

[SwaggerParameter(Description = "Optional: New category of the dish (Appetizer, MainCourse, Dessert, Drink)")]
public string? Category { get; set; }

[SwaggerParameter(Description = "Optional: List of ingredient IDs(number) that compose the dish. If not provided, the existing ingredients will be retained.")]
public ICollection<int>? DishIngredients { get; set; }
}
