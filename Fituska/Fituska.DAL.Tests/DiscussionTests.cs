using Nemesis.Essentials.Design;

namespace Fituska.DAL.Tests;
public class DiscussionTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public DiscussionTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(DiscussionTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewDiscussion()
    {
        //Arrange
        var newDiscussion = new DiscussionEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Sin + cos = arctg ?",
        };

        dbContext.Discussions.Add(newDiscussion);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedDiscussion = testDbContext.Discussions
            .FirstOrDefault(discussion => discussion.Id == newDiscussion.Id);
        Assert.StrictEqual(newDiscussion, retrievedDiscussion);
    }

    [Fact]
    public void AddNewDiscussionWithEntities()
    {
        //Arrange
        var newDiscussion = new DiscussionEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Sin + cos = arctg ?",
            Answer = new AnswerEntity()
            {
                Text = "Ano",
            },
            Author = new UserEntity()
            {
                FirstName = "Author",
            },
            Files = new ValueCollection<FileEntity>()
            {
                new FileEntity()
                {
                    Name = "Graph",
                    Content = new byte[]{1,2,3,4,5,6,7,8,9},
                }
            },
        };

        dbContext.Discussions.Add(newDiscussion);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedDiscussion = testDbContext.Discussions
            .Include(file => file.Files)
            .Include(file => file.Author)
            .Include(file => file.Answer)
            .FirstOrDefault(discussion => discussion.Id == newDiscussion.Id);
        Assert.StrictEqual(newDiscussion, retrievedDiscussion);
    }

    [Fact]
    public void AddNewRecurringDiscussion()
    {
        //Arrange
        var newDiscussion = new DiscussionEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "origin",
            Answer = new AnswerEntity()
            {
                Text = "Ano",
            },
        };
        var nestedDiscussion1 = new DiscussionEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Nested discussion 1",
        };
        var nestedDiscussion2 = new DiscussionEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Nested discussion 2",
        };
        dbContext.Discussions.Add(newDiscussion);
        nestedDiscussion1.OriginDiscussion = newDiscussion;
        nestedDiscussion1.OriginId = newDiscussion.Id;
        dbContext.Discussions.Add(nestedDiscussion1);
        nestedDiscussion2.OriginDiscussion = nestedDiscussion1;
        nestedDiscussion2.OriginId = nestedDiscussion1.Id;
        dbContext.Discussions.Add(nestedDiscussion2);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        DiscussionEntity? retrievedDiscussion = testDbContext.Discussions
            .Include(discussion => discussion.OriginDiscussion)
            .ThenInclude(discussion => discussion.OriginDiscussion)
            .FirstOrDefault(discussion => discussion.Id == nestedDiscussion2.Id);
        Assert.StrictEqual(nestedDiscussion2, retrievedDiscussion);
    }
}

