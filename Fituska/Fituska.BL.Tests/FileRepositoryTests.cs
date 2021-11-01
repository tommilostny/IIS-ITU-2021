namespace Fituska.BL.Tests;
using Microsoft.EntityFrameworkCore;

public class FileRepositoryTests
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    private readonly FileRepository fileRepository;
   
    public FileRepositoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(FileRepositoryTests));
        dbContext = dbContextFactory.Create();
        fileRepository = new FileRepository(dbContext);
    }

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    public async Task DisposeAsync() => await dbContext.Database.EnsureDeletedAsync();


    [Fact]
    public void GetFileById()
    {
        FileEntity file = SeedData();

        dbContext.Files.Add(file);
        dbContext.SaveChanges();

        var fileFromDb = fileRepository.GetByID(file.Id);
        Assert.StrictEqual(file, fileFromDb);
    }


    [Fact]
    public void InsertFiles()
    {
        FileEntity file = SeedData();

        fileRepository.Insert(file);
        using var database = dbContextFactory.Create();
        var fileFromDb = database.Files
            .First(fileToFind => fileToFind.Id == file.Id);
        Assert.StrictEqual(file, fileFromDb);
        database.Files.Remove(fileFromDb);
        database.SaveChanges();
    }
    
    [Fact]
    public void GetAllFiles()
    {
        dbContext.Files.RemoveRange(dbContext.Files);
        List<FileEntity> Files = new(){};
        Files.Add(SeedData());
        Files.Add(SeedData());
        dbContext.Files.AddRange(Files);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        List<FileEntity> FilesFromDb = (List<FileEntity>)fileRepository.GetAll();
        Assert.True(FilesFromDb.SequenceEqual(Files));
        Assert.NotStrictEqual(Files[0], FilesFromDb[1]);
        Assert.NotStrictEqual(Files[1], FilesFromDb[0]);
    }

    [Fact]
    public void UpdateFile()
    {
        FileEntity file = SeedData();
        dbContext.Files.Add(file);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var fileFromDb = database.Files
            .First(fileToFind => fileToFind.Id == file.Id);
        Assert.StrictEqual(file, fileFromDb);
        file.Content = new byte[0];
        file = (FileEntity)fileRepository.Update(file);
        var updatedUserFromDb = database.Files
            .First(fileToFind => fileToFind.Id == file.Id);
        Assert.StrictEqual(fileFromDb, updatedUserFromDb);
    }

    [Fact]
    public void DeleteFiles()
    {
        FileEntity file = SeedData();
        dbContext.Files.Add(file);
        dbContext.SaveChanges();
        using var database = dbContextFactory.Create();
        var fileFromDb = database.Files
            .First(fileToFind => fileToFind.Id == file.Id);
        Assert.StrictEqual(file, fileFromDb);
        fileRepository.Delete(file.Id);
        var deletedCategory = database.Files.FirstOrDefault(deletingUser => deletingUser.Id == file.Id);
        Assert.Null(deletedCategory);
    }

    public FileEntity SeedData()
    {
        FileEntity file = new()
        {
            Name = "Pùlsemestrálka",
            Content = new byte[100]
        };   
        return file;
    }
}
