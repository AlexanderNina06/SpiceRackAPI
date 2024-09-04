using System;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.IngredientRequests;

public class UpdateIngredientRequest
{

[SwaggerParameter(Description = "ID of the ingredient to update")]
public int Id { get; set; }

[SwaggerParameter(Description = "New name of the ingredient")]
public string Name { get; set; }
}
