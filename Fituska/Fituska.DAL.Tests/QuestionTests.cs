namespace Fituska.DAL.Tests;
public class QuestionTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public QuestionTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(QuestionTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewQuestion()
    {
        var newQuestion = new QuestionEntity
        {
            Text = "2+2?",
            Title = "Sčítání"
        };

        dbContext.Questions.Add(newQuestion);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedQuestion = testDbContext.Questions.SingleOrDefault(question => question.Id == newQuestion.Id);
        Assert.StrictEqual(newQuestion, retrievedQuestion);
    }

}

