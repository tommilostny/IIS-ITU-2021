using Microsoft.EntityFrameworkCore;
using Fituska.Server.Data;

namespace Fituska.Server.Factories;

public class InMemoryDbContextFactory : IDbContextFactory
{
    private readonly string _databaseName;

    public InMemoryDbContextFactory(string databaseName)
    {
        _databaseName = databaseName;
    }

    public FituskaDbContext Create()
    {
        var contextOptionsBuilder = new DbContextOptionsBuilder<FituskaDbContext>()
            .UseInMemoryDatabase(_databaseName);

        return new FituskaDbContext(contextOptionsBuilder.Options);
    }
}
