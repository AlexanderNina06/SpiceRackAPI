using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SpiceRack.Core.Application.DTOs.Account;
using SpiceRack.Core.Application.Interfaces.Services;
using SpiceRack.Core.Domain.Enums;
using SpiceRack.Core.Domain.Settings;
using SpiceRack.Infrastructure.Identity.Entities;
using System.Security.Cryptography;

namespace SpiceRack.Infrastructure.Identity.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<ApplicationUser> _userMangaer;
    private readonly SignInManager<ApplicationUser> _SignInManager;
    private readonly JWTSettings _jWTSettings;
    public AccountService(UserManager<ApplicationUser> userMangaer, 
    SignInManager<ApplicationUser> signInManager,
    IOptions<JWTSettings> jWTSettings)
    {
    _userMangaer = userMangaer;
    _SignInManager = signInManager;
    _jWTSettings = jWTSettings.Value;
    }

    #region Authenticate
    public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
    {
      AuthenticationResponse response = new();

      var user = await _userMangaer.FindByNameAsync(request.UserName);
      if(user == null)
      {
        response.HasError = true;
        response.Error = $"No account registered with {request.UserName} is found.";
        return response;
      }

      var result = await _SignInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
      if(!result.Succeeded)
      {
        response.HasError = true;
        response.Error= $"Invalid Credentials for {request.UserName}";
        return response;
      }
      JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);
      
      response.Id = user.Id;
      response.Email = user.Email;  
      response.UserName = user.UserName;
      var roleList = await _userMangaer.GetRolesAsync(user).ConfigureAwait(false);
      response.Roles = roleList.ToList();
      response.JWTToken =  new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
      var refreshToken = GenerateRefreshToken();
      response.RefreshToken = refreshToken.Token;
    
      return response;
    }
    #endregion

    #region Register
    public async Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin)
    {
        RegisterResponse response = new()
      {
        HasError = false
      };

      var userWithSameUserName = await _userMangaer.FindByNameAsync(request.UserName);
      if(userWithSameUserName != null)
      {
        response.HasError = true;
        response.Error = $"User name '{request.UserName}' already Exist.";
        return response;
      }

      var userWithSameEmail = await _userMangaer.FindByEmailAsync(request.Email);
      if(userWithSameEmail != null)
      {
        response.HasError = true;
        response.Error= $"Email {request.Email} is already registered.";
        return response;
      }

      if (!ValidatePassword(request.Password))
      {
        response.HasError = true;
        response.Error = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, a number, and a special character.";
        return response;
      }

      var user = new ApplicationUser
      {
        Email = request.Email,
        FirstName = request.FirstName,  
        LastName = request.LastName,  
        UserName = request.UserName,
      };

      var result = await _userMangaer.CreateAsync(user, request.Password);
      if(result.Succeeded)
      {
         await _userMangaer.AddToRoleAsync(user, Roles.Admin.ToString());
      }
      else
      {
        response.HasError = true;
        response.Error = "An error has ocurred during process to register your user";
        return response;
      }
      
      return response;
    }

    public async Task<RegisterResponse> RegisterWaitersUserAsync(RegisterRequest request, string origin)
    {
        RegisterResponse response = new()
      {
        HasError = false
      };

      var userWithSameUserName = await _userMangaer.FindByNameAsync(request.UserName);
      if(userWithSameUserName != null)
      {
        response.HasError = true;
        response.Error = $"User name '{request.UserName}' already Exist.";
        return response;
      }

      var userWithSameEmail = await _userMangaer.FindByEmailAsync(request.Email);
      if(userWithSameEmail != null)
      {
        response.HasError = true;
        response.Error= $"Email {request.Email} is already registered.";
        return response;
      }

      if (!ValidatePassword(request.Password))
      {
        response.HasError = true;
        response.Error = "Password must contain at least 8 characters, one uppercase letter, one lowercase letter, a number, and a special character.";
        return response;
      }

      var user = new ApplicationUser
      {
        Email = request.Email,
        FirstName = request.FirstName,  
        LastName = request.LastName,  
        UserName = request.UserName,
      };

      var result = await _userMangaer.CreateAsync(user, request.Password);
      if(result.Succeeded)
      {
         await _userMangaer.AddToRoleAsync(user, Roles.Waiters.ToString());
      }
      else
      {
        response.HasError = true;
        response.Error = "An error has ocurred during process to register your user";
        return response;
      }
      
      return response;
    }
    #endregion

    #region SignOut
    public async Task SignOutAsync()
    {
        await _SignInManager.SignOutAsync();
    }
    #endregion

    #region Private Methods
    private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
    {
      var userClaims = await _userMangaer.GetClaimsAsync(user);
      var roles = await _userMangaer.GetRolesAsync(user);
      var roleClaims = new List<Claim>();

      foreach(var role in roles)
      {
        roleClaims.Add(new Claim("roles", role));
      }

      var claims = new[]
      {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim("uid", user.Id)

      }
      .Union(userClaims)
      .Union(roleClaims);

      var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
      var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

      var JwtSecurityToken = new JwtSecurityToken(
        issuer: _jWTSettings.Issuer,
        audience: _jWTSettings.Audience,
        claims = claims,
        expires: DateTime.UtcNow.AddMinutes(_jWTSettings.DurationInMinutes),
        signingCredentials: signingCredentials
      );


      return JwtSecurityToken;
    }

    private RefreshToken GenerateRefreshToken()
    {
      return new RefreshToken
      {
        Token = RandomTokenString(),
        Expires = DateTime.UtcNow.AddDays(7),
        Created = DateTime.UtcNow,
      };
    }

    private string RandomTokenString()
    {
      using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
      var ramdomBytes = new byte[40];
      rngCryptoServiceProvider.GetBytes(ramdomBytes);

      return BitConverter.ToString(ramdomBytes).Replace("-", "");
    }

    private bool ValidatePassword(string password)
    {
    const int minLength = 8;
    var hasUpper = password.Any(char.IsUpper);
    var hasLower = password.Any(char.IsLower);
    var hasSymbol = password.Any(c => !char.IsLetterOrDigit(c));

    return password.Length >= minLength && hasUpper && hasLower && hasSymbol;
    } 

    #endregion

}
