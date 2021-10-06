using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fituska.Server.Entities.Interfaces;

namespace Fituska.Server.Repositories.Interfaces
{
    public interface IAppRepository<TEntity> where TEntity : IEntity
    {
        IList<TEntity> GetAll();
        TEntity GetById(Guid id);
        Guid Create(TEntity entity);
        Guid Update(TEntity entity);
        void Delete(Guid id);
    }
}
