using System;
using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace SpiceRack.Core.Application.DTOs.Requests.TableRequests;

public class CreateTableRequest
{

[SwaggerParameter(Description = "Number of people the table can accommodate")]
public int Capacity { get; set; }

/// <example> Round wooden table for 4 people, located on the terrace with a garden view. </example>
[SwaggerParameter("Description = Description of the table, including physical attributes (size, shape, material, location), capacity and usage information (number of people, type of use), restrictions or special notes (reservations, accessibility, maintenance), and aesthetic information (color, style).")]
public string Description { get; set; }
}
