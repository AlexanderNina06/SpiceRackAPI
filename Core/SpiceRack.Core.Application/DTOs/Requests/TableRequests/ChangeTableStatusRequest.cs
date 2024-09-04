using System;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.TableRequests;

public class ChangeTableStatusRequest
{

[SwaggerParameter(Description = "ID of the table")]
public int Id { get; set; }

/// <example>(One of these: Available, InProgress or Attended)</example>
[SwaggerParameter(Description = "New status for the table (Available, InProgress or Attended)")]
public string Status { get; set; } 
}
