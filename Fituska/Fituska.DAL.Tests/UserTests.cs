using Nemesis.Essentials.Design;

namespace Fituska.DAL.Tests;
public class UserTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    public UserTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(UserTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();
    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewUser()
    {
        //Arrange
        var newUser = new UserEntity
        {
            DiscordUsername = "RivolaCz",
            UserName = "RivolaCz",
            FirstName = "Michal",
            LastName = "Rivola",
            PasswordHash = "123456789",
            PhoneNumber = "737657683",
            EmailConfirmed = true,
            Photo = new byte[] { 1, 5, 10, 15 },
            AccessFailedCount = 2,
            RegistrationDate = new DateTime(2021, 10, 6, 22, 56, 59, 450),
            LastLoginDate = new DateTime(2021, 10, 6, 22, 57, 59, 450),
            Email = "m.rivola2@seznam.cz",
        };

        //Act
        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedUser = testDbContext.Users.SingleOrDefault(user => user.Id == newUser.Id);
        Assert.StrictEqual(newUser, retrievedUser);
    }

    [Fact]
    public void AddNewUserWithCourseAttendency()
    {
        //Arrange
        UserEntity? newUser = new()
        {
            FirstName = "Michal",
            LastName = "Rivola",
            DiscordUsername = "RivolaCz",
            Email = "email",
            PasswordHash = "password512hash",
            UserName = "Rivola",
            AttendingCourses = new ValueCollection<CourseAttendanceEntity>() {
                new CourseAttendanceEntity(){
                    Course = new CourseEntity
                    {
                        AcademicYear = 2021,
                        Credits = 5,
                        Description = "Description",
                        Name = "Signály a systémy",
                        Semester = Fituska.Shared.Enums.Semester.Winter,
                        Url = "www.sss.com",
                        Shortcut = "ISS"
                    },

                },
            }
        };

        //Act
        dbContext.Users.Add(newUser);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedUser = testDbContext.Users.Include(user => user.AttendingCourses).ThenInclude(attendingCourses => attendingCourses.Course).SingleOrDefault(user => user.Id == newUser.Id);
        Assert.Equal(newUser, retrievedUser);
    }
}

