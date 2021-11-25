namespace Fituska.BL.Repositories;

public class UserSawAnswerRespository : IRepository<UserSawAnswerEntity>
{
    private readonly FituskaDbContext database;

    public UserSawAnswerRespository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        var entity = database.UsersSawAnswers.Find(entityID);
        if (entity is not null)
        {
            database.UsersSawAnswers.Remove(entity);
            database.SaveChanges();
        }
    }

    public IEnumerable<UserSawAnswerEntity> GetAll()
    {
        return database.UsersSawAnswers
            .Include(usa => usa.Answer)
            .Include(usa => usa.User)
            .ToList();
    }

    public UserSawAnswerEntity GetByID(Guid entityID)
    {
        return database.UsersSawAnswers
            .Include(usa => usa.Answer)
            .Include(usa => usa.User)
            .FirstOrDefault(usa => usa.Id == entityID);
    }

    public UserSawAnswerEntity Insert(UserSawAnswerEntity entity)
    {
        var entityInDb = database.UsersSawAnswers
            .Where(e => e.UserId == entity.UserId)
            .FirstOrDefault(e => e.AnswerId == entity.AnswerId);

        if (entityInDb is null)
        {
            database.UsersSawAnswers.Add(entity);
            database.SaveChanges();
            return entity;
        }
        return entityInDb;
    }

    public UserSawAnswerEntity Update(UserSawAnswerEntity entity)
    {
        var entityToUpdate = database.UsersSawAnswers.Attach(entity);
        entityToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }
}
