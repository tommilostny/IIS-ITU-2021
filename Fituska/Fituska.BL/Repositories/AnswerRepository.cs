using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class AnswerRepository : IRepository<AnswerEntity>
{
    private readonly FituskaDbContext database;

    public AnswerRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        AnswerEntity? answer = database.Answers.Find(entityID);
        if (answer != null)
        {
            database.Answers.Remove(answer);
            database.SaveChanges();
        }
    }

    public IEntity Insert(IEntity model)
    {
        var answer = (AnswerEntity)model;
        database.Answers.Add(answer);
        database.SaveChanges();
        return answer;
    }

    public IEntity Update(IEntity model)
    {
        var answer = (AnswerEntity)model;
        var answerToUpdate = database.Answers.Attach(answer);
        answerToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return answer;
    }

    public IEnumerable<IEntity> GetAll()
    {
        return database.Answers
            .Include(answer => answer.UsersSawAnswer)
            .ThenInclude(userSawQuestion => userSawQuestion.User)
            .ToList();
    }
    public IEntity GetByID(Guid entityID)
    {
        var answer = database.Answers
            .Include(answer => answer.UsersSawAnswer)
            .ThenInclude(userSawQuestion => userSawQuestion.User)
            .First(answer => answer.Id == entityID);
        return answer;
    }
}
