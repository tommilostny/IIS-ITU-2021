using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class CategoryRepository : IRepository<CategoryEntity>
{
    private readonly FituskaDbContext database;

    public CategoryRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(IEntity entity)
    {
        var category = (CategoryEntity)entity;
        if (category != null)
        {
            database.Categories.Remove(category);
            database.SaveChanges();
        }
    }

    public void Delete(Guid entityID)
    {
        CategoryEntity? ansnwer = database.Categories.Find(entityID);
        Delete(ansnwer!);
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
            .First(category => category.Id == entityID);
        return category;
    }
}
