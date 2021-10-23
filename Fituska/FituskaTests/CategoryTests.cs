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
public class CategoryTests : IAsyncLifetime
{
    private readonly IDbContextFactory dbContextFactory;
    private readonly FituskaDbContext dbContext;

    public CategoryTests()
    {
        dbContextFactory = new InMemoryDbContextFactory(nameof(CategoryTests));
        dbContext = dbContextFactory.Create();
    }

    public async Task DisposeAsync() => await dbContext.DisposeAsync();

    public async Task InitializeAsync() => await dbContext.Database.EnsureCreatedAsync();

    [Fact]
    public void AddNewCategory()
    {
        //Arrange
        var newCategory = new CategoryEntity
        {
            Name = "Půlsemestrálka",
            Description = "Ptejte se k půlsemestrálce"
        };

        dbContext.Categories.Add(newCategory);
        dbContext.SaveChanges();

        //Assert
        using var testDbContext = dbContextFactory.Create();
        var retrievedCategory = testDbContext.Categories.SingleOrDefault(category => category.Id == newCategory.Id);
        Assert.StrictEqual(newCategory, retrievedCategory);
    }
}

