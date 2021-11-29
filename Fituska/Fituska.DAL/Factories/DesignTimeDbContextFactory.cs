using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Fituska.DAL.Factories;

internal class DesignTimeDbContextFactory : IDbContextFactory, IDesignTimeDbContextFactory<FituskaDbContext>
{
    public FituskaDbContext Create()
    {
        var builder = new DbContextOptionsBuilder<FituskaDbContext>();

    #if DEBUG
        builder.UseSqlite("Data Source=FituskaDb.db");
    #else
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory().Replace("Fituska.DAL", "Fituska.API"))
                .AddJsonFile("appsettings.json")
                .Build();

        builder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    #endif
        return new FituskaDbContext(builder.Options);
    }

    public FituskaDbContext CreateDbContext(string[] args) => Create();
}
