using SpiceRack.Infrastructure.Identity;
using SpiceRack.WebApi.Extensions;
using SpiceRack.Core.Application;
using Microsoft.AspNetCore.Identity;
using SpiceRack.Infrastructure.Identity.Entities;
using SpiceRack.Infrastructure.Identity.Seeds;
using SpiceRack.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityInfrastructureForApi(builder.Configuration);
builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers(options => 
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(options => 
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressMapClientErrors = true;
}).AddNewtonsoftJson();
builder.Services.AddHealthChecks();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


var app = builder.Build();

using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
		var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);
        await DefaultWaitersUser.SeedAsync(userManager, roleManager);

	}
	catch (Exception ex)
    {
//
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();  
}
app.UseHttpsRedirection();
app.UseRouting();   
app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerExtension();
app.UseErrorHandlingMiddleware();
app.UseHealthChecks("/health");
app.UseSession();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
