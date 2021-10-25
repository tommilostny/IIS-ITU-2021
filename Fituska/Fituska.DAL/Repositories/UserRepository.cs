using Fituska.DAL.Entities.Interfaces;
using Fituska.DAL.Factories;
namespace Fituska.DAL.Repositories
{
    public class UserRepository : IRepository<UserEntity>
    {
        private readonly FituskaDbContext database;
        public UserRepository(IDbContextFactory database)
        {
            this.database = (FituskaDbContext)database;
        }

        public void Delete(IEntity entity)
        {
            database.Users.Remove((UserEntity)entity);
        }

        public void Delete(Guid entityID)
        {
            UserEntity? user = database.Users.Find(entityID);
            Delete(user!);
        }

        public IEntity InsertOrUpdate(IEntity model)
        {
            throw new NotImplementedException();
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
}
