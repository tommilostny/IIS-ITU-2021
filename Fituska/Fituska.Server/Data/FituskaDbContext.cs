using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fituska.Server.Data;

public class FituskaDbContext : IdentityDbContext
{
    public FituskaDbContext(DbContextOptions<FituskaDbContext> options) : base(options)
    {
    }
}
