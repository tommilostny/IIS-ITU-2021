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
public class DiscussionTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public DiscussionTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(DiscussionTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewDiscussion()
    {
        //Arrange
        var newDiscussion = new DiscussionEntity
        {
            CreationTime = new DateTime(2021, 10, 22, 17, 16, 50, 150),
            Text = "Sin + cos = arctg ?",
        };

        dbContext.Discussions.Add(newDiscussion);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedDiscussion = testDbContext.Discussions.SingleOrDefault(discussion => discussion.Id == newDiscussion.Id);
        Assert.StrictEqual(newDiscussion, retrievedDiscussion);
    }
}

