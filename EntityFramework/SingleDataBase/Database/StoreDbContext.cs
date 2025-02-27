using Microsoft.EntityFrameworkCore;
using SingleDataBase.Database.Configurations;
using SingleDataBase.Entities;
using SingleDataBase.Services.Abstractions;

namespace SingleDataBase.Database;

public class StoreDbContext(DbContextOptions options) : DbContext(options)
{
    private readonly IStoreCodeProvider _storeCodeProvider = null!;
    private readonly ICurrentUserProvider _currentUserProvider = null!;

    public StoreDbContext(DbContextOptions options, IStoreCodeProvider storeCodeProvider, ICurrentUserProvider currentUserProvider)
        : this(options)
    {
        _storeCodeProvider = storeCodeProvider;
        _currentUserProvider = currentUserProvider;
    }

    public DbSet<Store> Stores { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreConfiguration).Assembly);

        modelBuilder.Entity<Store>()
            .HasQueryFilter(x => x.UserId == _currentUserProvider.UserId);

        modelBuilder.Entity<Deal>()
            .HasQueryFilter(x => x.StoreCode == _storeCodeProvider.StoreCode);

        modelBuilder.Entity<Product>()
            .HasQueryFilter(x => x.StoreCode == _storeCodeProvider.StoreCode);
    }
}
