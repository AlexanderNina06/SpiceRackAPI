using System;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.IngredientRequests;

public class CreateIngredientRequest
{
[SwaggerParameter(Description = "Name of the ingredient")]
public string Name { get; set; }
}
