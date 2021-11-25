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

    public CategoryEntity Insert(CategoryEntity entity)
    {
        database.Categories.Add(entity);
        database.SaveChanges();
        return entity;
    }

    public CategoryEntity Update(CategoryEntity entity)
    {
        var categoryToUpdate = database.Categories.Attach(entity);
        categoryToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public IEnumerable<CategoryEntity> GetAll()
    {
        return database.Categories
            .Include(category => category.Course)
            .Include(category => category.Questions)
            .ToList();
    }

    public CategoryEntity GetByID(Guid entityID)
    {
        return database.Categories
            .Include(category => category.Course)
            .Include(category => category.Questions)
            .FirstOrDefault(category => category.Id == entityID);
    }
}
