using Fituska.Server.Entities.Interfaces;
using Fituska.Server.Factories;
using Fituska.Server.Models.DetailModels;
using Fituska.Server.Models.ListModels;
using Microsoft.EntityFrameworkCore.Query;
using Fituska.Server.Data;
using System.Linq;

namespace Fituska.Server.Repositories
{
    public class UserRepository : IRepository<UserEntity>
    {
        private readonly FituskaDbContext database;
        public UserRepository(FituskaDbContext database)
        {
            this.database = database;
        }

        public void Delete(IEntity entity)
        {
            database.Users.Remove((UserEntity)entity);
        }

        public void Delete(Guid entityID)
        {
            UserEntity? user =  database.Users.Find(entityID);
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
