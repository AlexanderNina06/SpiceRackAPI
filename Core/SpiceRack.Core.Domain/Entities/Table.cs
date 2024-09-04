
using SpiceRack.Core.Domain.Common;
using SpiceRack.Core.Domain.Enums;

namespace SpiceRack.Core.Domain.Entities;

public class Table : AuditableBaseEntity
{
public int Capacity { get; set; }
public string Description { get; set; }
public TableStatus Status { get; set; }
 
}