namespace Fituska.DAL.Tests;
public class AnswerTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    public AnswerTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(AnswerTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();
    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewAnswer()
    {
        //Arrange
        var newAnswer = new AnswerEntity
        {
            CreationTime = new DateTime(2020, 10, 22, 12, 15, 6, 450),
            Text = "Ahoj",
        };

        dbContext.Answers.Add(newAnswer);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedAnswer = testDbContext.Answers.SingleOrDefault(answer => answer.Id == newAnswer.Id);
        Assert.StrictEqual(newAnswer, retrievedAnswer);
    }
}

