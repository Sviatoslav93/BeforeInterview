using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using SingleDataBase;
using SingleDataBase.Database;
using SingleDataBase.Endpoints;
using SingleDataBase.Middlewares;
using SingleDataBase.Services;
using SingleDataBase.Services.Abstractions;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddOpenApi()
    .AddCors()
    .AddAuthentication()
    .AddAuthorization()
    .AddDbContext();

builder.Services.AddHttpContextAccessor();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IStoreCodeProvider, StoreCodeProvider>();
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

#pragma warning disable EXTEXP0018
builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(10),
        LocalCacheExpiration = TimeSpan.FromMinutes(10)
    };
});
#pragma warning restore EXTEXP0018

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<CheckStoreCodeMiddleware>();

app.MapStoreEndpoints();
app.MapLoginEndpoints();
app.MapDealsEndpoints();
app.MapProductsEndpoints();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
