namespace Fituska.BL.Tests;
public class DiscussionRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly CommentRepository discussionRepository;

    public DiscussionRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(DiscussionRepositoryTests));
        dbContext = dbContextFactory.Create();
        discussionRepository = new CommentRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();
    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();

    [Fact]
    public void GetDiscussionById()
    {
        CommentEntity discussion = SeedData();
        dbContext.Comments.Add(discussion);
        dbContext.SaveChanges();
        CommentEntity? discussionFromDb = (CommentEntity?)discussionRepository.GetByID(discussion.Id);
        Assert.StrictEqual(discussion, discussionFromDb);
    }


    [Fact]
    public void InsertDiscussion()
    {
        CommentEntity discussion = SeedData();
        discussionRepository.Insert(discussion);
        using var database = dbContextFactory.Create();
        var discussionFromDb = database.Comments
            .FirstOrDefault(discussionToFind => discussionToFind.Id == discussion.Id);
        Assert.StrictEqual(discussion, discussionFromDb);
        database.Comments.Remove(discussionFromDb);
        database.SaveChanges();
    }

    [Fact]
    public void InsertRecursiveDiscussionAndGetById()
    {
        var originDiscussion = SeedData();
        discussionRepository.Insert(originDiscussion);
        var nestedDiscussion = SeedData(1);
        nestedDiscussion.ParentComment = originDiscussion;
        discussionRepository.Insert(nestedDiscussion);
        var nestedDiscussion2 = SeedData(1);
        nestedDiscussion2.ParentComment = nestedDiscussion;
        discussionRepository.Insert(nestedDiscussion2);
        using var database = dbContextFactory.Create();
        CommentEntity nestedDiscussion2FromDb = (CommentEntity)discussionRepository.GetByID(nestedDiscussion2.Id);
        Assert.StrictEqual(nestedDiscussion2, nestedDiscussion2FromDb);
    }

    [Fact]
    public void GetAllDiscussions()
    {
        dbContext.Comments.RemoveRange(dbContext.Comments);
        List<CommentEntity> Discussions = new() { };
        Discussions.Add(SeedData());
        Discussions.Add(SeedData());
        dbContext.Comments.AddRange(Discussions);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        List<CommentEntity> DiscussionFromDb = (List<CommentEntity>)discussionRepository.GetAll();

        Assert.True(DiscussionFromDb.SequenceEqual(Discussions));
        Assert.NotStrictEqual(Discussions[0], DiscussionFromDb[1]);
        Assert.NotStrictEqual(Discussions[1], DiscussionFromDb[0]);
    }

    [Fact]
    public void UpdateDiscussion()
    {
        CommentEntity discussion = SeedData();
        dbContext.Comments.Add(discussion);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var discussionFromDb = database.Comments
            .FirstOrDefault(answerToFind => answerToFind.Id == discussion.Id);
        Assert.StrictEqual(discussion, discussionFromDb);
        discussionFromDb.Text = "Updated text";
        discussion = (CommentEntity)discussionRepository.Update(discussion);
        var updatedUserFromDb = database.Comments
            .FirstOrDefault(answerToFind => answerToFind.Id == discussion.Id);
        Assert.StrictEqual(discussionFromDb, updatedUserFromDb);
    }

    [Fact]
    public void DeleteDiscussion()
    {
        CommentEntity discussion = SeedData();
        dbContext.Comments.Add(discussion);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var discussionFromDb = database.Comments
            .FirstOrDefault(answerToFind => answerToFind.Id == discussion.Id);
        Assert.StrictEqual(discussion, discussionFromDb);
        discussionRepository.Delete(discussionFromDb.Id);
        var deletedDiscussion = database.Comments.FirstOrDefault(deletingUser => deletingUser.Id == discussion.Id);
        discussionFromDb.Text = "Deleted";
        Assert.Equal("Deleted", deletedDiscussion?.Text);
        Assert.StrictEqual(discussionFromDb, deletedDiscussion);

    }

    public CommentEntity SeedData(int levelOfNesting = 0)
    {
        CommentEntity discussion = new()
        {
            Text = $"Diskuze {levelOfNesting}",
            //Files = new ValueCollection<FileEntity>()
            //{
            //    new FileEntity()
            //    {
            //        Name = $"File {levelOfNesting}",
            //        Content = new byte[10000]
            //    }
            //}
        };
        UserEntity author = new()
        {
            FirstName = $"User {levelOfNesting} first name",
            LastName = $"User {levelOfNesting}last name ",
            Email = $"user {levelOfNesting} email",
        };
        discussion.User = author;
        return discussion;
    }
}
