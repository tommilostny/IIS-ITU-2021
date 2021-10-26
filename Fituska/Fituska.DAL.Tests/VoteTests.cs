using Fituska.DAL.Factories;

namespace Fituska.DAL.Tests;
public class VoteTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public VoteTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(VoteTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewVote()
    {
        var newVote = new VoteEntity
        {
            Vote = Fituska.Shared.Enums.QuestionVote.Like,
        };

        dbContext.Votes.Add(newVote);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedVote = testDbContext.Votes.SingleOrDefault(vote => vote.Id == newVote.Id);
        Assert.StrictEqual(newVote, retrievedVote);
    }
}

