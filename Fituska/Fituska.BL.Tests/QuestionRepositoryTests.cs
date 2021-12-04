namespace Fituska.BL.Tests;
public class QuestionRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly QuestionRepository questionRepository;

    public QuestionRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(FileRepositoryTests));
        dbContext = dbContextFactory.Create();
        questionRepository = new QuestionRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();


    [Fact]
    public void GetQuestionById()
    {
        QuestionEntity question = SeedData();

        dbContext.Questions.Add(question);
        dbContext.SaveChanges();

        var questionFromDb = questionRepository.GetByID(question.Id);
        Assert.StrictEqual(question, questionFromDb);
    }


    [Fact]
    public void InsertQuestion()
    {
        QuestionEntity question = SeedData();

        questionRepository.Insert(question);
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
        ValueCollection<QuestionEntity> questions = new() { };
        questions.Add(SeedData());
        questions.Add(SeedData());
        dbContext.Questions.AddRange(questions);
        dbContext.SaveChanges();

        var fromDb = new ValueCollection<QuestionEntity>(questionRepository.GetAll().ToList());
        Assert.NotStrictEqual(questions[0], fromDb[1]);
        Assert.NotStrictEqual(questions[1], fromDb[0]);
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
        question = (QuestionEntity)questionRepository.Update(question);
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
        questionRepository.Delete(question.Id);
        var deletedCategory = database.Questions.FirstOrDefault(deletingUser => deletingUser.Id == question.Id);
        Assert.Null(deletedCategory);
    }

    public QuestionEntity SeedData()
    {
        QuestionEntity question = new()
        {
            Title = "Sčítání",
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
            },
            UserSawQuestions = new ValueCollection<UserSawQuestionEntity>()
            {
                new UserSawQuestionEntity()
                {
                    User = new UserEntity()
                    {
                        UserName = "administrator"
                    }
                }
            },
            CreationTime = new(2021, 10, 8),
            User = new UserEntity() { UserName = "xlogin01" }
        };
        return question;
    }
}
