using System;
using System.ComponentModel.DataAnnotations;

namespace SpiceRack.Core.Application.ViewModels.Users;

public class SaveUserViewModel
{
[Required(ErrorMessage = "Name field is required")]
[DataType(DataType.Text)]
public string FirstName { get; set; }

[Required(ErrorMessage = "Last name field is required")]
[DataType(DataType.Text)]
public string LastName { get; set; }

[Required(ErrorMessage = "Username field is required")]
[DataType(DataType.Text)]
public string UserName { get; set; }

[Required(ErrorMessage = "Password is required")]
[DataType(DataType.Password)]
public string Password { get; set; }

[Compare(nameof(Password),ErrorMessage ="The passwords are not the same")]
[Required(ErrorMessage = "Password is required")]
[DataType(DataType.Password)]
public string ConfirmPassword { get; set; }

[Required(ErrorMessage = "Email field is required")]
[DataType(DataType.EmailAddress)]
public string Email { get; set; }

[Required(ErrorMessage = "Phone field is required")]
[DataType(DataType.PhoneNumber)]
public string Phone { get; set; }

public string? UserType { get; set; }

public bool HasError { get; set; }
public string? Error { get; set; }
}
