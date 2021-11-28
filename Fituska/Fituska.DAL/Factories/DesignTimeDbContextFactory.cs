using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Fituska.DAL.Factories;

internal class DesignTimeDbContextFactory : IDbContextFactory, IDesignTimeDbContextFactory<FituskaDbContext>
{
    public FituskaDbContext Create()
    {
        var builder = new DbContextOptionsBuilder<FituskaDbContext>();

    #if DEBUG
        builder.UseSqlite("Data Source=FituskaDb.db");
    #else
        builder.UseSqlServer(
            "Server=tcp:fituska.database.windows.net,1433;Initial Catalog=fituska;Persist Security Info=False;User ID=fituska_admin;Password=xmilos02&team;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
        );
    #endif
        return new FituskaDbContext(builder.Options);
    }

    public FituskaDbContext CreateDbContext(string[] args) => Create();
}
