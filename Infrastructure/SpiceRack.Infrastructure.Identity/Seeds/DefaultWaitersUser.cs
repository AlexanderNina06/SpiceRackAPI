using System;
using Microsoft.AspNetCore.Identity;
using SpiceRack.Core.Domain.Enums;
using SpiceRack.Infrastructure.Identity.Entities;

namespace SpiceRack.Infrastructure.Identity.Seeds;

public static  class DefaultWaitersUser
{
public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultUser = new();
			defaultUser.UserName = "AdminUser";
			defaultUser.Email = "Adminemail@email.com";
			defaultUser.FirstName = "Carlos";
			defaultUser.LastName = "Jones";
			defaultUser.EmailConfirmed = true;
			defaultUser.PhoneNumberConfirmed = true;

			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "123Pa$$word!");
					await userManager.AddToRoleAsync(defaultUser, Roles.Waiters.ToString());					
				}

			}
		}
}
