using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.OpenApi.Models;

namespace SpiceRack.WebApi.Extensions;

public static class ServiceExtension
{
  public static void AddSwaggerExtension(this IServiceCollection services)
  {
    services.AddSwaggerGen(options =>
    {
      List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
      xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

      options.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "SpiceRack API",
        Description = "This Api will be responsible for overall data distribution",
        Contact = new OpenApiContact
        {
          Name = "Enrique Nina",
          Email = "Enriquenina06@hotmail.com"          
        }
      });

       options.DescribeAllParametersInCamelCase();

    });
  }

  public static void AddApiVersioningExtension(this IServiceCollection services)
  {
    services.AddApiVersioning(config =>
    {
      config.DefaultApiVersion = new ApiVersion(1, 0);
      config.AssumeDefaultVersionWhenUnspecified = true;
      config.ReportApiVersions = true;
    });
  }
}
