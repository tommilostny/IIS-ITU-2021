namespace Fituska.BL.Repositories;

public class CourseRepository : IRepository<CourseEntity>, ISearchableRepository<CourseEntity>
{
    private readonly FituskaDbContext database;

    public CourseRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CourseEntity> GetAll()
    {
        throw new NotImplementedException();
    }

    public CourseEntity GetByID(Guid entityID)
    {
        throw new NotImplementedException();
    }

    public CourseEntity Insert(CourseEntity entity)
    {
        throw new NotImplementedException();
    }

    public CourseEntity Update(CourseEntity entity)
    {
        throw new NotImplementedException();
    }

    public List<CourseEntity> Search(string searchTerm)
    {
        return database.Courses.Where(c => c.Name.Contains(searchTerm) || c.Shortcut.Contains(searchTerm)).ToList();
    }
}
