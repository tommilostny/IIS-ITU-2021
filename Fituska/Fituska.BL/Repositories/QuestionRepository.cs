using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class QuestionRepository : IRepository<QuestionEntity>
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

    public IEntity Insert(IEntity model)
    {
        var question = (QuestionEntity)model;
        database.Questions.Add(question);
        database.SaveChanges();
        return question;
    }

    public IEntity Update(IEntity model)
    {
        var question = (QuestionEntity)model;
        var questionToUpdate = database.Questions.Attach(question);
        questionToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return question;
    }

    public IEnumerable<IEntity> GetAll()
    {
        IEnumerable<IEntity> discussions = database.Questions
            .ToList();
        return discussions;
    }
    public IEntity GetByID(Guid entityID)
    {
        QuestionEntity? question = database.Questions.First(question => question.Id == entityID);
        return question;
    }
}
