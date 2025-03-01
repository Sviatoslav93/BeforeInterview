using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Store.Database;

public class StoreContextDesignFactory : IDesignTimeDbContextFactory<StoreDbContext>
{
    public StoreDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=StoreDb;User Id=sa;Password=Password123;",
            builder => builder.MigrationsAssembly(typeof(StoreDbContext).Assembly.FullName));

        return new StoreDbContext(optionsBuilder.Options);
    }
}
