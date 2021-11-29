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
            .Include(answer => answer.User)
            .Include(answer => answer.UsersSawAnswer)
            .Include(answer => answer.UsersVoteAnswers)
            .Include(answer => answer.Comments)
            .Include(answer => answer.Files)
            .ToList();
    }

    public AnswerEntity GetByID(Guid entityID)
    {
        var answer = database.Answers
            .Include(answer => answer.User)
            .Include(answer => answer.UsersSawAnswer)
            .Include(answer => answer.UsersVoteAnswers)
            .Include(answer => answer.Comments)
            .Include(answer => answer.Files)
            .FirstOrDefault(answer => answer.Id == entityID);

        answer.User = database.Users.FirstOrDefault(user => user.Id == answer.UserId);
        return answer;
    }

    public List<AnswerEntity> Search(string searchTerm)
    {
        return database.Answers.Where(a => a.Text.Contains(searchTerm)).ToList();
    }
}
