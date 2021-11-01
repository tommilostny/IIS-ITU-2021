namespace Fituska.DAL.Tests;
public class FileTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public FileTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(FileTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Theory]
    [InlineData(null)]
    [InlineData(new byte[] { 1, 2, 3, 4, 5 })]
    [InlineData(new byte[] { })]
    public void AddNewFile(byte[] content)
    {
        //Arrange
        var newFile = new FileEntity
        {
            Name = "luxusnívideosešumem.wav",
            Content = content,
        };
        dbContext.Files.Add(newFile);
        dbContext.SaveChanges();
        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedFile = testDbContext.Files
            .FirstOrDefault(file => file.Id == newFile.Id);
        Assert.StrictEqual(newFile, retrievedFile);
    }

    [Fact]
    public void AddNewFileWithAnswer()
    {
        var newFile = new FileEntity
        {
            Name = "luxusnívideosešumem.wav",
            Content = new byte[] { 1, 5, 6, 255 },
            Answer = new AnswerEntity()
            {
                CreationTime = new DateTime(2021, 12, 9),
                Text = "Tady je odpověď s obrázkem",
            },
        };
        dbContext.Files.Add(newFile);
        dbContext.SaveChanges();
        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedFile = testDbContext.Files
            .Include(file => file.Answer)
            .FirstOrDefault(file => file.Id == newFile.Id);
        Assert.StrictEqual(newFile, retrievedFile);
    }

    [Fact]
    public void AddNewFileWithDiscussion()
    {
        var newFile = new FileEntity
        {
            Name = "luxusnívideosešumem.wav",
            Content = new byte[] { 1, 5, 6, 255 },
            Discussion = new DiscussionEntity()
            {
                CreationTime = new DateTime(2021, 12, 9),
                Text = "Tady je odpověď s obrázkem",
            },
        };
        dbContext.Files.Add(newFile);
        dbContext.SaveChanges();
        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedFile = testDbContext.Files
            .Include(file => file.Discussion)
            .FirstOrDefault(file => file.Id == newFile.Id);
        Assert.StrictEqual(newFile, retrievedFile);
    }

    [Fact]
    public void AddNewFileWithQuestion()
    {
        var newFile = new FileEntity
        {
            Name = "luxusnívideosešumem.wav",
            Content = new byte[] { 1, 5, 6, 255 },
            Question = new QuestionEntity()
            {
                Text = "Text",
                Title = "Title",
            }
        };
        dbContext.Files.Add(newFile);
        dbContext.SaveChanges();
        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedFile = testDbContext.Files
            .Include(file => file.Question)
            .FirstOrDefault(file => file.Id == newFile.Id);
        Assert.StrictEqual(newFile, retrievedFile);
    }

}

