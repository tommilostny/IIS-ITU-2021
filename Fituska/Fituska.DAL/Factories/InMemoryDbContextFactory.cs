using Microsoft.EntityFrameworkCore;

namespace Fituska.DAL.Factories;

public class InMemoryDbContextFactory : IDbContextFactory
{
    private readonly string databaseName;

    public InMemoryDbContextFactory(string databaseName)
    {
        this.databaseName = databaseName;
    }

    public FituskaDbContext Create()
    {
        var contextOptionsBuilder = new DbContextOptionsBuilder<FituskaDbContext>();
        contextOptionsBuilder.UseInMemoryDatabase(databaseName)
            .EnableSensitiveDataLogging();

        return new FituskaDbContext(contextOptionsBuilder.Options);
    }
}
