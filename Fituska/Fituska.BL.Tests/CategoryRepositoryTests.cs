namespace Fituska.BL.Tests;
using Microsoft.EntityFrameworkCore;

public class CategoryRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly CategoryRepository categoryRepository;

    public CategoryRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(CategoryRepositoryTests));
        dbContext = dbContextFactory.Create();
        categoryRepository = new CategoryRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();


    [Fact]
    public void GetCategoryById()
    {
        CategoryEntity category = SeedData();

        dbContext.Categories.Add(category);
        dbContext.SaveChanges();

        var categoryFromDB = categoryRepository.GetByID(category.Id);
        Assert.StrictEqual(category, categoryFromDB);
    }


    [Fact]
    public void InsertCategory()
    {
        CategoryEntity category = SeedData();

        categoryRepository.Insert(category);
        using var database = dbContextFactory.Create();
        var categoryFromDB = database.Categories
            .Include(category => category.Course)
            .FirstOrDefault(answerToFind => answerToFind.Id == category.Id);
        Assert.StrictEqual(category, categoryFromDB);
        database.Categories.Remove(categoryFromDB);
        database.SaveChanges();
    }

    [Fact]
    public void GetAllCategories()
    {
        dbContext.Categories.RemoveRange(dbContext.Categories);
        List<CategoryEntity> Categories = new() { };
        Categories.Add(SeedData());
        Categories.Add(SeedData());
        dbContext.Categories.AddRange(Categories);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        List<CategoryEntity> AnswersFromDb = (List<CategoryEntity>)categoryRepository.GetAll();

        Assert.True(AnswersFromDb.SequenceEqual(Categories));
        Assert.NotStrictEqual(Categories[0], AnswersFromDb[1]);
        Assert.NotStrictEqual(Categories[1], AnswersFromDb[0]);
    }

    [Fact]
    public void UpdateCategory()
    {
        CategoryEntity category = SeedData();
        dbContext.Categories.Add(category);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var categoryFromDB = database.Categories
            .Include(categories => categories.Course)
            .FirstOrDefault(answerToFind => answerToFind.Id == category.Id);
        Assert.StrictEqual(category, categoryFromDB);
        categoryFromDB.Course.Semester = Shared.Enums.Semester.Summer;
        category = (CategoryEntity)categoryRepository.Update(category);
        var updatedUserFromDb = database.Categories
            .Include(categories => categories.Course)
            .FirstOrDefault(answerToFind => answerToFind.Id == category.Id);
        Assert.StrictEqual(categoryFromDB, updatedUserFromDb);
    }

    [Fact]
    public void DeleteCategory()
    {
        CategoryEntity category = SeedData();
        dbContext.Categories.Add(category);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var categoryFromDB = database.Categories
            .Include(category => category.Course)
            .FirstOrDefault(answerToFind => answerToFind.Id == category.Id);
        Assert.StrictEqual(category, categoryFromDB);
        categoryRepository.Delete(category.Id);
        var deletedCategory = database.Categories.FirstOrDefault(deletingUser => deletingUser.Id == category.Id);
        Assert.Null(deletedCategory);
    }

    public CategoryEntity SeedData()
    {
        CategoryEntity category = new()
        {
            Name = "Pùlsemestrálka",

        };
        CourseEntity course = new()
        {
            Name = "Signály a systémy",
            Description = "Fourierovo peklo",
            Shortcut = "ISS",
            Url = "bit3-2021-iss",
            YearOfStudy = Shared.Enums.YearOfStudy.BIT2,
            Semester = Shared.Enums.Semester.Winter,
            Credits = 6,
        };
        category.Course = course;
        return category;
    }
}
