using System;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.OrderRequests;

public class UpdateOrderRequest
{

[SwaggerParameter(Description = "ID of the order to update")]
public int Id { get; set; }

[SwaggerParameter(Description = "List of dish IDs (numbers) to add to the order.")]
public ICollection<int> Dishes { get; set; }
}
