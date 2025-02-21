using Microsoft.EntityFrameworkCore;
using SingleDataBase.Database.Configurations;
using SingleDataBase.Entities;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Database;

public class StoreContext(DbContextOptions options) : DbContext(options)
{
    private readonly IStoreCodeProvider _storeCodeProvider = null!;

    public StoreContext(DbContextOptions options, IStoreCodeProvider storeCodeProvider) : this(options)
    {
        _storeCodeProvider = storeCodeProvider;
    }

    public DbSet<Store> Stores { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new StoreConfiguration(_storeCodeProvider));
        modelBuilder.ApplyConfiguration(new DealConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}