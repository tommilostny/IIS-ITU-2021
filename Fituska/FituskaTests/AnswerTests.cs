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

public class AnswerTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;
    public AnswerTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(AnswerTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();
    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewAnswer()
    {
        //Arrange
        var newAnswer = new AnswerEntity
        {
            CreationTime = new DateTime(2020, 10, 22, 12, 15, 6, 450),
            Text = "Ahoj",
        };

        dbContext.Answers.Add(newAnswer);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedAnswer = testDbContext.Answers.SingleOrDefault(answer => answer.Id == newAnswer.Id);
        Assert.StrictEqual(newAnswer, retrievedAnswer);
    }
}

