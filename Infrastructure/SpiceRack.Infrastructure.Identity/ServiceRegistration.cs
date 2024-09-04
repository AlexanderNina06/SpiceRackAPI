using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SpiceRack.Core.Application.Exceptions;
using SpiceRack.Core.Application.Interfaces.Services;
using SpiceRack.Core.Application.Wrappers;
using SpiceRack.Core.Domain.Settings;
using SpiceRack.Infrastructure.Identity.Contexts;
using SpiceRack.Infrastructure.Identity.Entities;
using SpiceRack.Infrastructure.Identity.Services;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace SpiceRack.Infrastructure.Identity;

public static class ServiceRegistration
{
public static async Task AddIdentityInfrastructureForApi(this IServiceCollection services,IConfiguration configuration)
{

    ContextConfiguration(services,configuration);

    #region Identity

    services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>()
        .AddDefaultTokenProviders();

    services.Configure<JWTSettings>(configuration.GetSection("JWTSettings"));

    services.AddAuthentication(options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = configuration["JWTSettings:Issuer"],
            ValidAudience= configuration["JWTSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
        };
        options.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = c=>
            {
                c.NoResult();
                c.Response.StatusCode = 500;
                c.Response.ContentType = "text/plain";
                return c.Response.WriteAsync(c.Exception.ToString());
            },
            OnChallenge = c =>
            {
                c.HandleResponse();
                c.Response.StatusCode = 401;
                c.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized"));
                return c.Response.WriteAsync(result);
            },
            OnForbidden = c =>
            {
                c.Response.StatusCode = 403;
                c.Response.ContentType = "application/json";
                var result = JsonConvert.SerializeObject(new Response<string>("You are not authorized to access this results"));
                return c.Response.WriteAsync(result);
            }
        }; 
    });

    #endregion
    
    ServiceConfiguration(services);
}

#region private methods
    private static void ContextConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        #region Contexts

        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<IdentityContext>(options => options.UseInMemoryDatabase("IdentityDb"));
        }
        else
        {
            services.AddDbContext<IdentityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"),
            m => m.MigrationsAssembly(typeof(IdentityContext).Assembly.FullName)));
        }

        #endregion
    }
    private static void ServiceConfiguration(this IServiceCollection services)
    {
        #region Service
        
        services.AddTransient<IAccountService, AccountService>();

        #endregion
    }

#endregion

}
