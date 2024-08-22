using SpiceRack.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthChecks();
builder.Services.AddControllers();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();    
}else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();   
app.UseAuthentication();
app.UseAuthentication();

app.UseSwaggerExtension();

app.UseHealthChecks("/health");
app.UseSession();

app.Run();
