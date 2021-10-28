namespace Fituska.BL.Repositories;

public interface IRepository<TEntity> where TEntity : IEntity
{
    IEntity Insert(IEntity model);
    IEntity Update(IEntity model);
    void Delete(IEntity entity);
    void Delete(Guid entityID);
    IEntity GetByID(Guid entityID);
    IEnumerable<IEntity> GetAll();
}
