using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Tests;
public class VoteRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly VoteRepository voteRepository;

    public VoteRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(VoteRepositoryTests));
        dbContext = dbContextFactory.Create();
        voteRepository = new VoteRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();


    [Fact]
    public void GetVoteById()
    {
        VoteEntity vote = SeedData();

        dbContext.Votes.Add(vote);
        dbContext.SaveChanges();

        var voteFromDb = voteRepository.GetByID(vote.Id);
        Assert.StrictEqual(vote, voteFromDb);
    }


    [Fact]
    public void InsertVote()
    {
        VoteEntity vote = SeedData();

        voteRepository.Insert(vote);
        using var database = dbContextFactory.Create();
        var voteFromDb = database.Votes
            .Include(vote => vote.Answer)
            .Include(vote => vote.User)
            .FirstOrDefault(fileToFind => fileToFind.Id == vote.Id);
        Assert.StrictEqual(vote, voteFromDb);
        database.Votes.Remove(voteFromDb);
        database.SaveChanges();
    }

    [Fact]
    public void GetAllVote()
    {
        dbContext.Votes.RemoveRange(dbContext.Votes);
        List<VoteEntity> Votes = new() { };
        Votes.Add(SeedData());
        Votes.Add(SeedData());
        dbContext.Votes.AddRange(Votes);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        List<VoteEntity> VotesFromDb = (List<VoteEntity>)voteRepository.GetAll();
        Assert.True(VotesFromDb.SequenceEqual(Votes));
        Assert.NotStrictEqual(Votes[0], VotesFromDb[1]);
        Assert.NotStrictEqual(Votes[1], VotesFromDb[0]);
    }

    [Fact]
    public void UpdateVote()
    {
        VoteEntity vote = SeedData();
        dbContext.Votes.Add(vote);
        dbContext.SaveChanges();
        using (var database = dbContextFactory.Create())
        {
            var voteFromDb = database.Votes
                .Include(vote => vote.Answer)
                .Include(vote => vote.User)
                .FirstOrDefault(fileToFind => fileToFind.Id == vote.Id);
            Assert.StrictEqual(vote, voteFromDb);
            vote.Vote = Shared.Enums.VoteValue.Downvote;
            vote = (VoteEntity)voteRepository.Update(vote);
        }
        using (var database = dbContextFactory.Create())
        {
            var updatedVoteFromDb = database.Votes
            .Include(vote => vote.Answer)
            .Include(vote => vote.User)
            .FirstOrDefault(voteToFind => voteToFind.Id == vote.Id);
            Assert.StrictEqual(vote, updatedVoteFromDb);
        }

    }

    [Fact]
    public void DeleteVote()
    {
        VoteEntity vote = SeedData();
        dbContext.Votes.Add(vote);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var voteFromDb = database.Votes
            .Include(vote => vote.Answer)
            .Include(vote => vote.User)
            .FirstOrDefault(fileToFind => fileToFind.Id == vote.Id);
        Assert.StrictEqual(vote, voteFromDb);
        voteRepository.Delete(vote.Id);
        var deletedCategory = database.Votes.FirstOrDefault(deletingUser => deletingUser.Id == vote.Id);
        Assert.Null(deletedCategory);
    }

    public VoteEntity SeedData()
    {
        VoteEntity vote = new()
        {
            Answer = new AnswerEntity()
            {
                Text = "4",
                CreationTime = new DateTime(2021, 12, 9),
            },
            User = new UserEntity()
            {
                FirstName = "Liker",
            },
            Vote = Shared.Enums.VoteValue.Upvote,
        };
        return vote;
    }
}
