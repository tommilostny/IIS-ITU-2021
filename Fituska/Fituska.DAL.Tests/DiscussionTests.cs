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
        var newDiscussion = new CommentEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Sin + cos = arctg ?",
        };

        dbContext.Comments.Add(newDiscussion);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedDiscussion = testDbContext.Comments
            .FirstOrDefault(discussion => discussion.Id == newDiscussion.Id);
        Assert.StrictEqual(newDiscussion, retrievedDiscussion);
    }

    [Fact]
    public void AddNewDiscussionWithEntities()
    {
        //Arrange
        var newDiscussion = new CommentEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Sin + cos = arctg ?",
            Answer = new AnswerEntity()
            {
                Text = "Ano",
            },
            User = new UserEntity()
            {
                FirstName = "Author",
            }
        };

        dbContext.Comments.Add(newDiscussion);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedDiscussion = testDbContext.Comments
            .Include(file => file.User)
            .Include(file => file.Answer)
            .FirstOrDefault(discussion => discussion.Id == newDiscussion.Id);
        Assert.StrictEqual(newDiscussion, retrievedDiscussion);
    }

    [Fact]
    public void AddNewRecurringDiscussion()
    {
        //Arrange
        var newDiscussion = new CommentEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "origin",
            Answer = new AnswerEntity()
            {
                Text = "Ano",
            },
        };
        var nestedDiscussion1 = new CommentEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Nested discussion 1",
        };
        var nestedDiscussion2 = new CommentEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Nested discussion 2",
        };
        dbContext.Comments.Add(newDiscussion);
        nestedDiscussion1.ParentComment = newDiscussion;
        nestedDiscussion1.ParentCommentId = newDiscussion.Id;
        dbContext.Comments.Add(nestedDiscussion1);
        nestedDiscussion2.ParentComment = nestedDiscussion1;
        nestedDiscussion2.ParentCommentId = nestedDiscussion1.Id;
        dbContext.Comments.Add(nestedDiscussion2);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        CommentEntity? retrievedDiscussion = testDbContext.Comments
            .Include(discussion => discussion.ParentComment)
            .ThenInclude(discussion => discussion!.ParentComment)
            .FirstOrDefault(discussion => discussion.Id == nestedDiscussion2.Id);
        Assert.StrictEqual(nestedDiscussion2, retrievedDiscussion);
    }
}

