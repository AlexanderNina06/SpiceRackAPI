using System;

namespace SpiceRack.Core.Application.DTOs.Account;

public class JwtResponse
{
public bool HasError { get; set; }
public string Error { get; set; }
}
