using Fituska.Server.Data;

namespace Fituska.Server.Factories;

public interface IDbContextFactory
{
    FituskaDbContext Create();
}
