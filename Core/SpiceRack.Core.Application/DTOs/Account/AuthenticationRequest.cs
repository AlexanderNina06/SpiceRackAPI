using System;

namespace SpiceRack.Core.Application.DTOs.Account;

public class AuthenticationRequest
{
public string UserName { get; set; }
public string Password { get; set; }
}
