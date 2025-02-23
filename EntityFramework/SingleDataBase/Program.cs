using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SingleDataBase;
using SingleDataBase.Database;
using SingleDataBase.Endpoints;
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
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IStoreCodeProvider, StoreCodeProvider>();
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapStoreEndpoints();
app.MapLoginEndpoints();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    dbContext.Database.Migrate();
}

app.Run();
