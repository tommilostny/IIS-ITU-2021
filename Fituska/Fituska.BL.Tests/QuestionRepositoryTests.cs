namespace Fituska.BL.Tests;
public class QuestionRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly QuestionRepository fileRepository;

    public QuestionRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(FileRepositoryTests));
        dbContext = dbContextFactory.Create();
        fileRepository = new QuestionRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();


    [Fact]
    public void GetQuestionById()
    {
        QuestionEntity question = SeedData();

        dbContext.Questions.Add(question);
        dbContext.SaveChanges();

        var questionFromDb = fileRepository.GetByID(question.Id);
        Assert.StrictEqual(question, questionFromDb);
    }


    [Fact]
    public void InsertQuestion()
    {
        QuestionEntity question = SeedData();

        fileRepository.Insert(question);
        using var database = dbContextFactory.Create();
        var questionFromDb = database.Questions
            .FirstOrDefault(fileToFind => fileToFind.Id == question.Id);
        Assert.StrictEqual(question, questionFromDb);
        database.Questions.Remove(questionFromDb);
        database.SaveChanges();
    }

    [Fact]
    public void GetAllQuestions()
    {
        dbContext.Questions.RemoveRange(dbContext.Questions);
        List<QuestionEntity> Questions = new() { };
        Questions.Add(SeedData());
        Questions.Add(SeedData());
        dbContext.Questions.AddRange(Questions);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        List<QuestionEntity> FilesFromDb = (List<QuestionEntity>)fileRepository.GetAll();
        Assert.True(FilesFromDb.SequenceEqual(Questions));
        Assert.NotStrictEqual(Questions[0], FilesFromDb[1]);
        Assert.NotStrictEqual(Questions[1], FilesFromDb[0]);
    }

    [Fact]
    public void UpdateQuestion()
    {
        QuestionEntity question = SeedData();
        dbContext.Questions.Add(question);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var questionFromDb = database.Questions
            .FirstOrDefault(fileToFind => fileToFind.Id == question.Id);
        Assert.StrictEqual(question, questionFromDb);
        question.Title = "Updated Title";
        question = (QuestionEntity)fileRepository.Update(question);
        var updatedUserFromDb = database.Questions
            .FirstOrDefault(fileToFind => fileToFind.Id == question.Id);
        Assert.StrictEqual(questionFromDb, updatedUserFromDb);
    }

    [Fact]
    public void DeleteQuestion()
    {
        QuestionEntity question = SeedData();
        dbContext.Questions.Add(question);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var questionFromDb = database.Questions
            .FirstOrDefault(fileToFind => fileToFind.Id == question.Id);
        Assert.StrictEqual(question, questionFromDb);
        fileRepository.Delete(question.Id);
        var deletedCategory = database.Questions.FirstOrDefault(deletingUser => deletingUser.Id == question.Id);
        Assert.Null(deletedCategory);
    }

    public QuestionEntity SeedData()
    {
        QuestionEntity question = new()
        {
            Title = "Sèítání",
            Text = "Kolik je 2+2",
            Answers = new ValueCollection<AnswerEntity>()
            {
                new AnswerEntity()
                {
                    Text = "9",
                }
            },
            Category = new CategoryEntity()
            {
                Name = "Pùlsemestrálka",
                Description = "Description",
            },
            UserSawQuestions = new ValueCollection<UserSawQuestion>()
            {
                new UserSawQuestion()
                {
                    User = new UserEntity()
                    {

                    }
                }
            },
            CreationTime = new(2021, 10, 8),
        };
        return question;
    }
}
