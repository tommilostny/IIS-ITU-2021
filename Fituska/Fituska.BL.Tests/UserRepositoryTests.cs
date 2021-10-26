namespace Fituska.BL.Tests;

public class UserRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly UserRepository userRepository;
    private readonly IMapper mapper;
    public UserRepositoryTests(IMapper mapper)
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(UserRepositoryTests));
        dbContext = dbContextFactory.Create();
        userRepository = new UserRepository(dbContext);
        this.mapper = mapper;
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.DisposeAsync();
   
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
        var usersFromDb = database.Users.ToList();

        //TODO :D
        //List<UserListModel> listModels = new List<MusicBandListModel>();
        //foreach (MusicBandEntity bandFromDb in bandsFromDb)
        //{
        //    listModels.Add(MusicBandMapper.MapEntityToListModel(bandFromDb));
        //}
    }
}
