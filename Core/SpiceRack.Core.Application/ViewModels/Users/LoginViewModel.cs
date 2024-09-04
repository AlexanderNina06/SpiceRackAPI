using System;
using System.ComponentModel.DataAnnotations;

namespace SpiceRack.Core.Application.ViewModels.Users;

public class LoginViewModel
{
[Required(ErrorMessage = "Username is required")]
[DataType(DataType.Text)]
public string UserName { get; set; }

[Required(ErrorMessage = "Password is required")]
[DataType(DataType.Password)]
public string Password { get; set; }
public bool HasError { get; set; }
public string? Error { get; set; }
}
