using System;
using System.Runtime.CompilerServices;

namespace SpiceRack.Core.Application.DTOs.Responses.TableResponses;

public class GetTableOrderResponse
{
  public int TableNumber { get; set; }
  public int OrderNumber { get; set; }
  public decimal Subtotal { get; set; }
  public string Status { get; set; }
  public ICollection<string> Dishes { get; set; }

}
