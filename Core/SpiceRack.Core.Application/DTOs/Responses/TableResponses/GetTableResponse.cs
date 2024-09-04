using System;

namespace SpiceRack.Core.Application.DTOs.Responses.TableResponses;

public class GetTableResponse
{
public int Id { get; set; }
public int Capacity { get; set; }
public string Description { get; set; }
public string Status { get; set; }
}
