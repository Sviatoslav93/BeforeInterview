using Microsoft.EntityFrameworkCore;

namespace SingleDataBase.Database;

public class StoreContextInitializer(
    ILogger<StoreContextInitializer> logger,
    StoreDbContext context)
{
    public async Task InitialiseAsync()
    {
        try
        {
            if (context.Database.IsSqlServer())
            {
                await context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initialising the database");
            throw;
        }
    }
}
