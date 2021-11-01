namespace Fituska.BL.Tests;
public class DiscussionRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly DiscussionRepository discussionRepository;

    public DiscussionRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(DiscussionRepositoryTests));
        dbContext = dbContextFactory.Create();
        discussionRepository = new DiscussionRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();
    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();

    [Fact]
    public void GetDiscussionById()
    {
        DiscussionEntity discussion = SeedData();
        dbContext.Discussions.Add(discussion);
        dbContext.SaveChanges();
        DiscussionEntity? discussionFromDb = (DiscussionEntity?)discussionRepository.GetByID(discussion.Id);
        Assert.StrictEqual(discussion, discussionFromDb);
    }


    [Fact]
    public void InsertDiscussion()
    {
        DiscussionEntity discussion = SeedData();
        discussionRepository.Insert(discussion);
        using var database = dbContextFactory.Create();
        var discussionFromDb = database.Discussions
            .FirstOrDefault(discussionToFind => discussionToFind.Id == discussion.Id);
        Assert.StrictEqual(discussion, discussionFromDb);
        database.Discussions.Remove(discussionFromDb);
        database.SaveChanges();
    }

    [Fact]
    public void InsertRecursiveDiscussionAndGetById()
    {
        var originDiscussion = SeedData();
        discussionRepository.Insert(originDiscussion);
        var nestedDiscussion = SeedData(1);
        nestedDiscussion.OriginDiscussion = originDiscussion;
        discussionRepository.Insert(nestedDiscussion);
        var nestedDiscussion2 = SeedData(1);
        nestedDiscussion2.OriginDiscussion = nestedDiscussion;
        discussionRepository.Insert(nestedDiscussion2);
        using var database = dbContextFactory.Create();
        DiscussionEntity nestedDiscussion2FromDb = (DiscussionEntity)discussionRepository.GetByID(nestedDiscussion2.Id);
        Assert.StrictEqual(nestedDiscussion2, nestedDiscussion2FromDb);
    }

    [Fact]
    public void GetAllDiscussions()
    {
        dbContext.Discussions.RemoveRange(dbContext.Discussions);
        List<DiscussionEntity> Discussions = new() { };
        Discussions.Add(SeedData());
        Discussions.Add(SeedData());
        dbContext.Discussions.AddRange(Discussions);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        List<DiscussionEntity> DiscussionFromDb = (List<DiscussionEntity>)discussionRepository.GetAll();

        Assert.True(DiscussionFromDb.SequenceEqual(Discussions));
        Assert.NotStrictEqual(Discussions[0], DiscussionFromDb[1]);
        Assert.NotStrictEqual(Discussions[1], DiscussionFromDb[0]);
    }

    [Fact]
    public void UpdateDiscussion()
    {
        DiscussionEntity discussion = SeedData();
        dbContext.Discussions.Add(discussion);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var discussionFromDb = database.Discussions
            .FirstOrDefault(answerToFind => answerToFind.Id == discussion.Id);
        Assert.StrictEqual(discussion, discussionFromDb);
        discussionFromDb.Text = "Updated text";
        discussion = (DiscussionEntity)discussionRepository.Update(discussion);
        var updatedUserFromDb = database.Discussions
            .FirstOrDefault(answerToFind => answerToFind.Id == discussion.Id);
        Assert.StrictEqual(discussionFromDb, updatedUserFromDb);
    }

    [Fact]
    public void DeleteDiscussion()
    {
        DiscussionEntity discussion = SeedData();
        dbContext.Discussions.Add(discussion);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var discussionFromDb = database.Discussions
            .FirstOrDefault(answerToFind => answerToFind.Id == discussion.Id);
        Assert.StrictEqual(discussion, discussionFromDb);
        discussionRepository.Delete(discussionFromDb.Id);
        var deletedDiscussion = database.Discussions.FirstOrDefault(deletingUser => deletingUser.Id == discussion.Id);
        discussionFromDb.Text = "Deleted";
        Assert.Equal("Deleted", deletedDiscussion?.Text);
        Assert.StrictEqual(discussionFromDb, deletedDiscussion);

    }

    public DiscussionEntity SeedData(int levelOfNesting = 0)
    {
        DiscussionEntity discussion = new()
        {
            Text = $"Diskuze {levelOfNesting}",
            Files = new ValueCollection<FileEntity>()
            {
                new FileEntity()
                {
                    Name = $"File {levelOfNesting}",
                    Content = new byte[10000]
                }
            }
        };
        UserEntity author = new()
        {
            FirstName = $"User {levelOfNesting} first name",
            LastName = $"User {levelOfNesting}last name ",
            Email = $"user {levelOfNesting} email",
        };
        discussion.Author = author;
        return discussion;
    }
}
