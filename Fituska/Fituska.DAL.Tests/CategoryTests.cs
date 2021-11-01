namespace Fituska.DAL.Tests;
public class CategoryTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public CategoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(CategoryTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewCategory()
    {
        //Arrange
        var newCategory = new CategoryEntity
        {
            Name = "Půlsemestrálka",
            Description = "Ptejte se k půlsemestrálce",
            Course = new CourseEntity()
            {
                Name = "Signály a systémy",
                Shortcut = "ISS",               
            }
        };
        dbContext.Categories.Add(newCategory);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedCategory = testDbContext.Categories
            .Include(category => category.Course)
            .First(category => category.Id == newCategory.Id);
        Assert.StrictEqual(newCategory, retrievedCategory);
    }
}

