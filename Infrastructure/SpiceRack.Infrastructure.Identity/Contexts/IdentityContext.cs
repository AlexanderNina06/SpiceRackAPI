using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpiceRack.Infrastructure.Identity.Entities;

namespace SpiceRack.Infrastructure.Identity.Contexts;

public class IdentityContext : IdentityDbContext<ApplicationUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options) : base(options){}

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
      base.OnModelCreating(modelBuilder);
      //Fluent API

      modelBuilder.HasDefaultSchema("Identity");

      modelBuilder.Entity<ApplicationUser>(entity =>
      {
        entity.ToTable(name: "User");
      });

			modelBuilder.Entity<IdentityRole>(entity =>
			{
				entity.ToTable(name: "Roles");
			});

			modelBuilder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.ToTable(name: "UserRoles");
			});

			modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.ToTable(name: "UserLogins");
			});

		}
}
