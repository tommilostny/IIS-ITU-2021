namespace FituskaTests.DAL;
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
}

