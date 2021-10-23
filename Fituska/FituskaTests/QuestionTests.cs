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
public class QuestionTests :IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public QuestionTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(QuestionTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewQuestion()
    {
        var newQuestion = new QuestionEntity
        {
            Text = "2+2?",
            Title = "Sčítání"
        };

        dbContext.Question.Add(newQuestion);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedQuestion = testDbContext.Question.SingleOrDefault(question => question.Id == newQuestion.Id);
        Assert.StrictEqual(newQuestion, retrievedQuestion);
    }

}

