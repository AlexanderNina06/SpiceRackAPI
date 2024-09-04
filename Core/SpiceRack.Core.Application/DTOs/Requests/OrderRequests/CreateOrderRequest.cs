using System;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.OrderRequests;

public class CreateOrderRequest
{
///<example>(Id = 1) table #1 /-TableNumer : 1. </example>
[SwaggerParameter(Description = "Number of the table where the order is placed")]
public int TableNumber { get; set; }

/// <example>1(Id). Dish: Flat meat.2(Id). Dish: Pizza. / Dishes: 1, 2  =  Flat meat and pizza.</example>
[SwaggerParameter(Description = "List of dish IDs(numbers) included in the order")]
public ICollection<int> Dishes { get; set; }

}
