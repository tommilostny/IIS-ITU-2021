namespace FituskaTests.DAL;

public class CourseAttendenceTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public CourseAttendenceTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(CourseAttendenceTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewCourseAttendence()
    {
        //Arrange
        var newCourseAttendence = new CourseAttendanceEntity
        {
            AttendingYear = 2020,
        };

        dbContext.CourseAttendances.Add(newCourseAttendence);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedCourse = testDbContext.CourseAttendances.SingleOrDefault(courseAttendence => courseAttendence.Id == newCourseAttendence.Id);
        Assert.StrictEqual(newCourseAttendence, retrievedCourse);
    }
}

