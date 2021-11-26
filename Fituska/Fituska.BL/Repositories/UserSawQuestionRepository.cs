namespace Fituska.BL.Repositories;

public class UserSawQuestionRepository : IRepository<UserSawQuestionEntity>
{
    private readonly FituskaDbContext database;

    public UserSawQuestionRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        var entity = database.UsersSawQuestions.Find(entityID);
        if (entity is not null)
        {
            database.UsersSawQuestions.Remove(entity);
            database.SaveChanges();
        }
    }

    public IEnumerable<UserSawQuestionEntity> GetAll()
    {
        return database.UsersSawQuestions
            .Include(usq => usq.Question)
            .Include(usq => usq.User)
            .ToList();
    }

    public UserSawQuestionEntity GetByID(Guid entityID)
    {
        return database.UsersSawQuestions
            .Include(usq => usq.Question)
            .Include(usq => usq.User)
            .FirstOrDefault(usq => usq.Id == entityID);
    }

    public UserSawQuestionEntity Insert(UserSawQuestionEntity entity)
    {
        var entityInDb = database.UsersSawQuestions
            .Where(e => e.UserId == entity.UserId)
            .FirstOrDefault(e => e.QuestionId == entity.QuestionId);

        if (entityInDb is null)
        {
            database.UsersSawQuestions.Add(entity);
            database.SaveChanges();
            return entity;
        }
        return entityInDb;
    }

    public UserSawQuestionEntity Update(UserSawQuestionEntity entity)
    {
        var entityToUpdate = database.UsersSawQuestions.Attach(entity);
        entityToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }
}
