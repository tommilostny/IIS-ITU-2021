using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class AnswerRepository : IRepository<AnswerEntity>, ISearchableRepository<AnswerEntity>
{
    private readonly FituskaDbContext database;

    public AnswerRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        AnswerEntity? answer = database.Answers.Find(entityID);
        if (answer is not null)
        {
            database.Answers.Remove(answer);
            database.SaveChanges();
        }
    }

    public AnswerEntity Insert(AnswerEntity entity)
    {
        database.Answers.Add(entity);
        database.SaveChanges();
        return entity;
    }

    public AnswerEntity Update(AnswerEntity entity)
    {
        var answerToUpdate = database.Answers.Attach(entity);
        answerToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public IEnumerable<AnswerEntity> GetAll()
    {
        return database.Answers
            .Include(answer => answer.UsersSawAnswer)
            .ThenInclude(userSawQuestion => userSawQuestion.User)
            .Include(answer => answer.UsersVoteAnswers)
            .ToList();
    }

    public AnswerEntity GetByID(Guid entityID)
    {
        var answer = database.Answers
            .Include(answer => answer.UsersSawAnswer)
            .ThenInclude(userSawQuestion => userSawQuestion.User)
            .Include(answer => answer.UsersVoteAnswers)
            .FirstOrDefault(answer => answer.Id == entityID);
        return answer;
    }

    public List<AnswerEntity> Search(string searchTerm)
    {
        return database.Answers.Where(a => a.Text.Contains(searchTerm)).ToList();
    }
}
