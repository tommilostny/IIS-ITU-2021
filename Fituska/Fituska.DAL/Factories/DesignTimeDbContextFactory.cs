using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fituska.DAL.Factories;

internal class DesignTimeDbContextFactory : IDbContextFactory, IDesignTimeDbContextFactory<FituskaDbContext>
{
    public FituskaDbContext Create()
    {
        var builder = new DbContextOptionsBuilder<FituskaDbContext>();
        builder.UseSqlite("Data Source=FituskaDb.db");

        return new FituskaDbContext(builder.Options);
    }

    public FituskaDbContext CreateDbContext(string[] args) => Create();
}
