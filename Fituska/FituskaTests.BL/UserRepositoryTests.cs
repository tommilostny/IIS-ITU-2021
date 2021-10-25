namespace FituskaTests.BL;
public class UserRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly UserRepository userRepository;

    public UserRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(UserRepositoryTests));
        dbContext = dbContextFactory.Create();
        userRepository = new UserRepository(dbContextFactory);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.DisposeAsync();
   
    [Fact]
    public void Test1()
    {

    }
}
