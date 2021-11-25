namespace Fituska.BL.Repositories;

public interface IRepository<TEntity> where TEntity : IEntity
{
    TEntity Insert(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(Guid entityID);
    TEntity GetByID(Guid entityID);
    IEnumerable<TEntity> GetAll();
}
