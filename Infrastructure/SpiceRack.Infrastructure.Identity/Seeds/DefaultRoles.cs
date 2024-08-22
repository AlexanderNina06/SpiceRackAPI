using System;
using Microsoft.AspNetCore.Identity;
using SpiceRack.Core.Domain.Enums;
using SpiceRack.Infrastructure.Identity.Entities;

namespace SpiceRack.Infrastructure.Identity.Seeds;

public static class DefaultRoles
{
  public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
	{
    await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));

		await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

		await roleManager.CreateAsync(new IdentityRole(Roles.Waiters.ToString()));
	}
}
