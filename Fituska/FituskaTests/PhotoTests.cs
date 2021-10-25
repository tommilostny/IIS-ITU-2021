using Fituska.DAL;
using Fituska.DAL.Factories;

namespace FituskaTests.DAL;
public class PhotoTests: IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public PhotoTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(PhotoTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Theory]
    [InlineData(null)]
    [InlineData(new byte[] { 1, 2, 3, 4, 5 })]
    [InlineData(new byte[] { })]
    public void AddNewPhoto(byte[] content)
    {
        //Arrange
        var newPhoto = new PhotoEntity
        {
            Content = content
        };

        dbContext.Photos.Add(newPhoto);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedFile = testDbContext.Photos.SingleOrDefault(photo => photo.Id == newPhoto.Id);
        Assert.StrictEqual(newPhoto, retrievedFile);
    }
}

