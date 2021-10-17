using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fituska.Server.Data;

namespace Fituska.Server.Factories
{
    public interface IDbContextFactory
    {
        ApplicationDbContext Create();
    }
}
