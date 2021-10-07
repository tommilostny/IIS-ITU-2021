using Microsoft.EntityFrameworkCore;
using Fituska.Server.Data;

namespace Fituska.Server.Factories
{
    public class InMemoryDbContextFactory : IDbContextFactory
    {
        private readonly string databaseName;

        public InMemoryDbContextFactory(string databaseName)
        {
            this.databaseName = databaseName;
        }

        public ApplicationDbContext Create()
        {
            var contextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            contextOptionsBuilder.UseInMemoryDatabase(databaseName).;

            return new ApplicationDbContext(contextOptionsBuilder.Options);
        }
    }
}
