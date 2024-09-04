using System;

namespace SpiceRack.Core.Application.DTOs.Responses.OrderResponses;

public class GetOrderResponse
{
public int Id { get; set; }
public int TableNumber { get; set; }
public decimal Subtotal { get; set; }
public string Status{ get; set; }
public ICollection<string> Dishes { get; set; }

}
