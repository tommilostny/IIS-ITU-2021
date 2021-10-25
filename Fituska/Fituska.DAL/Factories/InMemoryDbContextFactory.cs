using Microsoft.EntityFrameworkCore;

namespace Fituska.DAL.Factories;
public class InMemoryDbContextFactory : IDbContextFactory
{
    private readonly string _databaseName;

    public InMemoryDbContextFactory(string databaseName)
    {
        _databaseName = databaseName;
    }

    public FituskaDbContext Create()
    {
        var contextOptionsBuilder = new DbContextOptionsBuilder<FituskaDbContext>();
        contextOptionsBuilder.UseInMemoryDatabase(_databaseName);

        return new FituskaDbContext(contextOptionsBuilder.Options);
    }
}
