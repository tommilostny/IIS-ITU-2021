namespace Fituska.BL.Tests;

public class AnswerRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly AnswerRepository answerRepository;
   
    public AnswerRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(AnswerRepositoryTests));
        dbContext = dbContextFactory.Create();
        answerRepository = new AnswerRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();


    [Fact]
    public void GetAnswerById()
    {
        AnswerEntity answer = SeedData();

        dbContext.Answers.Add(answer);
        dbContext.SaveChanges();

        var answerFromDB = answerRepository.GetByID(answer.Id);
        Assert.StrictEqual(answer, answerFromDB);
    }


    [Fact]
    public void InsertAnswer()
    {
        AnswerEntity answer = SeedData();

        answerRepository.Insert(answer);
        using var database = dbContextFactory.Create();
        var answerFromDB = database.Answers.Find(answer.Id);
        Assert.StrictEqual(answer, answerFromDB);
        database.Answers.Remove(answerFromDB);
        database.SaveChanges();
    }
    // TODO : Answers persisting through tests
    [Fact]
    public void GetAllAnswers()
    {
        List<AnswerEntity> Answers = new()
        {
            new AnswerEntity
            {
            },
            new AnswerEntity
            {
            },
            
        };
        dbContext.Answers.AddRange(Answers);
        dbContext.SaveChanges();

        using var database = dbContextFactory.Create();
        List<AnswerEntity> AnswersFromDb = (List<AnswerEntity>)answerRepository.GetAll();

        Assert.True(AnswersFromDb.SequenceEqual(Answers));
        Assert.NotStrictEqual(Answers[0], AnswersFromDb[1]);
        Assert.NotStrictEqual(Answers[1], AnswersFromDb[0]);
    }

    [Fact]
    public void UpdateAnswer()
    {
        AnswerEntity answer = SeedData();
        dbContext.Answers.Add(answer);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var answerFromDB = database.Answers.Find(answer.Id);
        Assert.StrictEqual(answer, answerFromDB);
        //answerFromDB.FirstName = "UpdatedName";
        //answerFromDB.LastName = "UpdatedLastName";
        //answerFromDB.DiscordUsername = "UpdatedDiscordUsername";
        //answerFromDB.PasswordHash = "UpdatedPasswordHash";
        answer = (AnswerEntity)answerRepository.Update(answer);
        var updatedUserFromDb = database.Answers.Find(answer.Id);
        Assert.StrictEqual(answerFromDB, updatedUserFromDb);
    }

    [Fact]
    public void DeleteAnswerByEntityReference()
    {
        AnswerEntity answer = SeedData();
        dbContext.Answers.Add(answer);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var answerFromDB = database.Answers.Find(answer.Id);
        Assert.StrictEqual(answer, answerFromDB);
        answerRepository.Delete(answer);
        var deletedAnswer = database.Answers.FirstOrDefault(deletingUser => deletingUser.Id == answer.Id);
        Assert.Null(deletedAnswer);
    }

    [Fact]
    public void DeleteAnswerById()
    {
        AnswerEntity answer = SeedData();
        dbContext.Answers.Add(answer);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var answerFromDB = database.Answers.Find(answer.Id);
        Assert.StrictEqual(answer, answerFromDB);
        answerRepository.Delete(answer.Id);
        var deletedUser = database.Answers.FirstOrDefault(deletingUser => deletingUser.Id == answer.Id);
        Assert.Null(deletedUser);
    }

    public AnswerEntity SeedData()
    {
        AnswerEntity answer = new()
        {
            Text = "Odpovìï je 4",
            User = new UserEntity()
            {
                FirstName = "Rivola"
            },
            CreationTime = new DateTime(2021, 10, 5, 17, 31, 50),
            UsersSawAnswer = new Nemesis.Essentials.Design.ValueCollection<UserSawAnswer>()
        };
        UserSawAnswer userSawAnswer = new()
        {
            User = new UserEntity()
            {
                FirstName = "Hater1",
                LastName = "Hater1"
            },
            Answer = answer,
        };
        answer.UsersSawAnswer.Add(userSawAnswer);
        return answer;
    }
}
