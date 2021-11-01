using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class CategoryRepository : IRepository<CategoryEntity>
{
    private readonly FituskaDbContext database;

    public CategoryRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        CategoryEntity? category = database.Categories.Find(entityID);
        if (category != null)
        {
            database.Categories.Remove(category);
            database.SaveChanges();
        }
    }

    public IEntity Insert(IEntity model)
    {
        var category = (CategoryEntity)model;
        database.Categories.Add(category);
        database.SaveChanges();
        return category;
    }

    public IEntity Update(IEntity model)
    {
        var category = (CategoryEntity)model;
        var answerToUpdate = database.Categories.Attach(category);
        answerToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return category;
    }

    public IEnumerable<IEntity> GetAll()
    {
        return database.Categories
            .Include(category => category.Course)
            .ToList();
    }
    public IEntity GetByID(Guid entityID)
    {
        var category = database.Categories
            .Include(category => category.Course)
            .FirstOrDefault(category => category.Id == entityID);
        return category;
    }
}
