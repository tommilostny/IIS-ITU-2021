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
        var entity = database.Courses.Find(entityID);
        if (entity is not null)
        {
            database.Courses.Remove(entity);
            database.SaveChanges();
        }
    }

    public IEnumerable<CourseEntity> GetAll()
    {
        return database.Courses
            .Include(course => course.Lecturer)
            .Include(course => course.Attendees)
            .Include(course => course.Categories)
            .ToList();
    }

    public CourseEntity GetByID(Guid entityID)
    {
        return database.Courses
            .Include(c => c.Lecturer)
            .Include(c => c.Attendees)
            .Include(c => c.Categories)
            .FirstOrDefault(usa => usa.Id == entityID);
    }

    public CourseEntity Insert(CourseEntity entity)
    {
        database.Courses.Add(entity);
        database.SaveChanges();
        return entity;
    }

    public CourseEntity Update(CourseEntity entity)
    {
        var questionToUpdate = database.Courses.Attach(entity);
        questionToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public List<CourseEntity> Search(string searchTerm)
    {
        return database.Courses.Where(c => c.Name.Contains(searchTerm) || c.Shortcut.Contains(searchTerm)).ToList();
    }
}
