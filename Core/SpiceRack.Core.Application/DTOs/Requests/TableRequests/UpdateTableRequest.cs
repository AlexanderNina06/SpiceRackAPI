using System;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.TableRequests;

public class UpdateTableRequest
{

[SwaggerParameter(Description = "ID of the table to update")]
public int Id { get; set; }

[SwaggerParameter(Description = "Optional: New capacity of the table")]
public int? Capacity { get; set; }

[SwaggerParameter(Description = "Optional: New description of the table")]
public string? Description { get; set; }
}
