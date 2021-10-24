using Fituska.Server.Entities.Interfaces;
using Fituska.Server.Models;

namespace Fituska.Server.Repositories;
public interface IRepository<TEntity> where TEntity: IEntity
{
    IEntity InsertOrUpdate(IEntity model);
    void Delete(IEntity entity);
    void Delete(Guid entityID);
    IEntity GetByID(Guid entityID);
    IEnumerable<IEntity> GetAll();
}