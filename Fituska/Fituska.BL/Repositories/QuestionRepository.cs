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
        QuestionEntity? question = database.Questions.FirstOrDefault(question => question.Id == entityID);
        if (question != null)
        {
            database.Questions.Remove(question);
            try
            {
                database.SaveChanges();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
        var questions = database.Questions
            .Include(question => question.UserSawQuestions)
            .Include(question => question.Answers)
            .Include(question => question.User)
            .Include(question => question.Category)
            .Include(answer => answer.Files)
            .ToList();
        return questions;
    }

    public QuestionEntity GetByID(Guid entityID)
    {
        QuestionEntity? question = database.Questions
            .Include(question => question.UserSawQuestions)
            .Include(question => question.Answers)
            .Include(question => question.User)
            .Include(question => question.Category)
            .Include(question => question.Files)
            .FirstOrDefault(question => question.Id == entityID);
        return question;
    }

    public List<QuestionEntity> Search(string searchTerm)
    {
        return database.Questions.Where(q => q.Title.Contains(searchTerm) || q.Text.Contains(searchTerm)).ToList();
    }
}
