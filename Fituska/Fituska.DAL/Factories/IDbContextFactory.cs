using Fituska.Server.Data;

namespace Fituska.DAL.Factories;

public interface IDbContextFactory
{
    FituskaDbContext Create();
}
