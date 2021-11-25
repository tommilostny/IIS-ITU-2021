namespace Fituska.DAL.Tests;
public class CourseTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public CourseTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(CourseTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewCourse()
    {
        //Arrange
        var newCourse = new CourseEntity
        {
            Name = "Signály a systémy",
            Description = "Fourierovo peklo",
            Credits = 6,
            Semester = Fituska.Shared.Enums.Semester.Summer,
            Shortcut = "ISS",
            Url = "www.google.com",
            YearOfStudy = Fituska.Shared.Enums.YearOfStudy.BIT2
        };

        dbContext.Courses.Add(newCourse);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedCourse = testDbContext.Courses.SingleOrDefault(course => course.Id == newCourse.Id);
        Assert.StrictEqual(newCourse, retrievedCourse);
    }
}

