using System;
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpiceRack.Core.Application.Interfaces.Services;
using SpiceRack.Core.Application.Mappings;

namespace SpiceRack.Core.Application;

public static class ServiceRegistration
{
public static void AddApplicationLayer(this IServiceCollection services, IConfiguration configuration)
{
  
	services.AddAutoMapper(Assembly.GetExecutingAssembly());
  services.AddMediatR(Assembly.GetExecutingAssembly());

}

}
