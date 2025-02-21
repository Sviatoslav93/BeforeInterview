using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SingleDataBase.Database;

public class StoreContextDesignFactory : IDesignTimeDbContextFactory<StoreContext>
{
    public StoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=StoreDb;User Id=sa;Password=Password123;",
            builder => builder.MigrationsAssembly(typeof(StoreContext).Assembly.FullName));

        return new StoreContext(optionsBuilder.Options);
    }
}
