using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class UserRepository : IRepository<UserEntity>
{
    private readonly FituskaDbContext database;

    public UserRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(IEntity entity)
    {
        var user = (UserEntity)entity;
        if (user != null)
        {
            database.Users.Remove(user);
            database.SaveChanges();
        }
    }

    public void Delete(Guid entityID)
    {
        UserEntity? user = database.Users.Find(entityID);
        Delete(user!);
    }

    public IEntity Insert(IEntity model)
    {
        UserEntity user = (UserEntity)model;
        database.Users.Add(user);
        database.SaveChanges();
        return user;
    }

    public IEntity Update(IEntity model)
    {
        UserEntity user = (UserEntity)model;
        var manufacturerForUpdate = database.Users.Attach(user);
        manufacturerForUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return user;
    }

    public IEnumerable<IEntity> GetAll()
    {
        return database.Users.ToList();
    }
    public IEntity GetByID(Guid entityID)
    {
        var user = database.Users.First(user => user.Id == entityID);
        return user;
    }
}
