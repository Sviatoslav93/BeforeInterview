using Microsoft.EntityFrameworkCore;
using Store.Database.Configurations;
using Store.Entities;
using Store.Services.Abstractions;

namespace Store.Database;

public class StoreDbContext(DbContextOptions options) : DbContext(options)
{
    private readonly IStoreIdProvider _storeIdProvider = null!;
    private readonly ICurrentUserProvider _currentUserProvider = null!;

    public StoreDbContext(DbContextOptions options, IStoreIdProvider storeIdProvider, ICurrentUserProvider currentUserProvider)
        : this(options)
    {
        _storeIdProvider = storeIdProvider;
        _currentUserProvider = currentUserProvider;
    }

    public DbSet<Entities.Store> Stores { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreConfiguration).Assembly);

        modelBuilder.Entity<Entities.Store>()
            .HasQueryFilter(x => x.UserId == _currentUserProvider.UserId);

        modelBuilder.Entity<Deal>()
            .HasQueryFilter(x => x.StoreId == _storeIdProvider.StoreId);

        modelBuilder.Entity<Product>()
            .HasQueryFilter(x => x.StoreId == _storeIdProvider.StoreId);
    }
}
