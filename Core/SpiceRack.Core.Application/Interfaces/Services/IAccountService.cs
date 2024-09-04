using System;
using SpiceRack.Core.Application.DTOs.Account;

namespace SpiceRack.Core.Application.Interfaces.Services;

public interface IAccountService
{
Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
Task<RegisterResponse> RegisterWaitersUserAsync(RegisterRequest request, string origin);
Task SignOutAsync();

}
