using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
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

       options.EnableAnnotations();
       options.DescribeAllParametersInCamelCase();
       options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
       {
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer",
          BearerFormat = "JWT",
          Description = "Input yor Bearer token in this format - Bearer {your token here}"
       });
       options.AddSecurityRequirement(new OpenApiSecurityRequirement
       {
          {
            new OpenApiSecurityScheme
            {
              Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "Bearer",
              Name = "Bearer",
              In = ParameterLocation.Header, 
            }, new List<string>()
          },
       });

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

        public static void AddControllersWithNewtonsoftJson(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
        }
}
