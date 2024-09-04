using System.Runtime.Serialization;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;


namespace SpiceRack.Core.Application.DTOs.Account;

public class AuthenticationResponse
{
public string Id { get; set; }
public string UserName { get; set; }
public string Email { get; set; }
public List<string> Roles { get; set; }
public bool IsVerified { get; set; }
public bool HasError { get; set; }
public string Error { get; set; }
public string JWTToken { get; set; }

[IgnoreDataMember]
public string RefreshToken { get; set; }

}
