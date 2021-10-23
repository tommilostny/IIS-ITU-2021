using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Fituska.Server.Data;
using Fituska.Server.Entities;
using Fituska.Server.Factories;

namespace FituskaTests.DAL;
public class FileTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public FileTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(FileTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Theory]
    [InlineData(null)]
    [InlineData(new byte[] { 1, 2, 3, 4, 5 })]
    [InlineData(new byte[] { })]
    public void AddNewFile(byte[] content)
    {
        //Arrange
        var newFile = new FileEntity
        {
            Name = "luxusnívideosešumem.wav",
            AnswerId = new Guid("55949177-2e41-4e1d-8b6f-7d50ac8190cf"),
            Content = content
        };
        dbContext.Files.Add(newFile);
        dbContext.SaveChanges();
        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedFile = testDbContext.Files.SingleOrDefault(file => file.Id == newFile.Id);
        Assert.StrictEqual(newFile, retrievedFile);
    }

}

