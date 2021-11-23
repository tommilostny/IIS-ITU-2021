using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class QuestionRepository : IRepository<QuestionEntity>, ISearchableRepository<QuestionEntity>
{
    private readonly FituskaDbContext database;

    public QuestionRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        QuestionEntity? question = database.Questions.Find(entityID);
        if (question != null)
        {
            database.Questions.Remove(question);
            database.SaveChanges();
        }
    }

    public QuestionEntity Insert(QuestionEntity entity)
    {
        database.Questions.Add(entity);
        database.SaveChanges();
        return entity;
    }

    public QuestionEntity Update(QuestionEntity entity)
    {
        var questionToUpdate = database.Questions.Attach(entity);
        questionToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public IEnumerable<QuestionEntity> GetAll()
    {
        IEnumerable<QuestionEntity> discussions = database.Questions
            .Include(question => question.UserSawQuestions)
            .Include(question => question.Answers)
            .ToList();
        return discussions;
    }

    public QuestionEntity GetByID(Guid entityID)
    {
        QuestionEntity? question = database.Questions
            .Include(question => question.UserSawQuestions)
            .Include(question => question.Answers)
            .FirstOrDefault(question => question.Id == entityID);
        return question;
    }

    public List<QuestionEntity> Search(string searchTerm)
    {
        return database.Questions.Where(q => q.Title.Contains(searchTerm) || q.Text.Contains(searchTerm)).ToList();
    }
}
