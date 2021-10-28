namespace Fituska.BL.Tests;

public class UserRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly UserRepository userRepository;
   
    public UserRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(UserRepositoryTests));
        dbContext = dbContextFactory.Create();
        userRepository = new UserRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();


    [Fact]
    public void GetUserById()
    {
        UserEntity user = new()
        {
            FirstName = "Name",
            LastName = "Surname",
            AccessFailedCount = 0,
            DiscordUsername = "DiscordName",
            Email = "email",
            EmailConfirmed = true,
            RegistrationDate = new DateTime(2021, 10, 10, 10, 59, 50),
        };
        UserEntity wrongUser = new()
        {
            FirstName = "WrongName",
            LastName = "WrongSurname",
            AccessFailedCount = 0,
            DiscordUsername = "WrongDiscordName",
            Email = "WrongEmail",
            EmailConfirmed = true,
            RegistrationDate = new DateTime(2021, 10, 10, 10, 59, 50),
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        var userFromDb = userRepository.GetByID(user.Id);
        Assert.StrictEqual(user, userFromDb);
        Assert.NotStrictEqual(user,wrongUser);
    }


    [Fact]
    public void InsertUser()
    {
        UserEntity user = new()
        {
            FirstName = "Name",
            LastName = "Surname",
            AccessFailedCount = 0,
            DiscordUsername = "DiscordName",
            Email = "email",
            EmailConfirmed = true,
            RegistrationDate = new DateTime(2021, 10, 10, 10, 59, 50),
        };

        userRepository.Insert(user);
        using var database = dbContextFactory.Create();
        var userFromDb = database.Users.Find(user.Id);
        Assert.StrictEqual(user, userFromDb);
    }
    // TODO : Users persisting through tests
    [Fact]
    public void GetAllUsers()
    {
        List<UserEntity> users = new()
        {
            new UserEntity
            {
                FirstName = "Name",
                LastName = "Surname",
                AccessFailedCount = 0,
                DiscordUsername = "DiscordName",
                Email = "email",
                EmailConfirmed = true,
                RegistrationDate = new DateTime(2021, 10, 10, 10, 59, 50),
            },
            new UserEntity
            {
                FirstName = "Name2",
                LastName = "Surname2",
                AccessFailedCount = 4,
                DiscordUsername = "DiscordName2",
                Email = "email3",
                EmailConfirmed = false,
                RegistrationDate = new DateTime(2020, 10, 10, 10, 59, 50),
            }
        };
        dbContext.Users.AddRange(users);
        dbContext.SaveChanges();

        using var database = dbContextFactory.Create();
        List<UserEntity> usersFromDb = (List<UserEntity>)userRepository.GetAll();

        Assert.True(usersFromDb.SequenceEqual(users));
        /*for (int i = 0; i < users.Count; i++)
        {
            Assert.StrictEqual(users[i],usersFromDb[i]);
        }
        Assert.NotStrictEqual(users[0], usersFromDb[1]);
        Assert.NotStrictEqual(users[1], usersFromDb[0]);*/
    }

    [Fact]
    public void UpdateUser()
    {
        UserEntity user = new()
        {
            FirstName = "Name",
            LastName = "Surname",
            AccessFailedCount = 0,
            DiscordUsername = "DiscordName",
            Email = "email",
            EmailConfirmed = true,
            RegistrationDate = new DateTime(2021, 10, 10, 10, 59, 50),
        };
        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var userFromDb = database.Users.Find(user.Id);
        Assert.StrictEqual(user, userFromDb);
        userFromDb.FirstName = "UpdatedName";
        userFromDb.LastName = "UpdatedLastName";
        userFromDb.DiscordUsername = "UpdatedDiscordUsername";
        userFromDb.PasswordHash = "UpdatedPasswordHash";
        user = (UserEntity)userRepository.Update(user);
        var updatedUserFromDb = database.Users.Find(user.Id);
        Assert.StrictEqual(userFromDb, updatedUserFromDb);
    }

    [Fact]
    public void DeleteUserByEntityReference()
    {
        UserEntity user = new()
        {
            FirstName = "Name",
            LastName = "Surname",
            AccessFailedCount = 0,
            DiscordUsername = "DiscordName",
            Email = "email",
            EmailConfirmed = true,
            RegistrationDate = new DateTime(2021, 10, 10, 10, 59, 50),
        };

        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var userFromDb = database.Users.Find(user.Id);
        Assert.StrictEqual(user, userFromDb);
        userRepository.Delete(user);
        var deletedUser = database.Users.FirstOrDefault(deletingUser => deletingUser.Id == user.Id);
        Assert.Null(deletedUser);
    }

    [Fact]
    public void DeleteUserById()
    {
        UserEntity user = new()
        {
            FirstName = "Name",
            LastName = "Surname",
            AccessFailedCount = 0,
            DiscordUsername = "DiscordName",
            Email = "email",
            EmailConfirmed = true,
            RegistrationDate = new DateTime(2021, 10, 10, 10, 59, 50),
        };

        dbContext.Users.Add(user);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var userFromDb = database.Users.Find(user.Id);
        Assert.StrictEqual(user, userFromDb);
        userRepository.Delete(user.Id);
        var deletedUser = database.Users.FirstOrDefault(deletingUser => deletingUser.Id == user.Id);
        Assert.Null(deletedUser);
    }
}
