using System;
using Microsoft.AspNetCore.Identity;
using SpiceRack.Core.Domain.Enums;
using SpiceRack.Infrastructure.Identity.Entities;

namespace SpiceRack.Infrastructure.Identity.Seeds;

public static class DefaultSuperAdminUser
{
public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			ApplicationUser defaultUser = new();
			defaultUser.UserName = "SuperAdminUser";
			defaultUser.Email = "SuperAdmin@gmail.com";
			defaultUser.FirstName = "Alex";
			defaultUser.LastName = "Williams";
			defaultUser.EmailConfirmed = true;
			defaultUser.PhoneNumberConfirmed = true;

			if (userManager.Users.All(u => u.Id != defaultUser.Id))
			{
				var user = await userManager.FindByEmailAsync(defaultUser.Email);
				if (user == null)
				{
					await userManager.CreateAsync(defaultUser, "123456Pa$$word!");
					await userManager.AddToRoleAsync(defaultUser, Roles.Waiters.ToString());
					await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
          await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());					
				}

			}
		}
}
