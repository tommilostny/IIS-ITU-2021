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
            .Include(course => course.Lecturer)
            .Include(course => course.Attendees)
            .Include(course => course.Categories)
            .FirstOrDefault(course => course.Id == entityID);
    }

    public CourseEntity GetByUrl(string courseUrl)
    {
        return database.Courses
            .Include(course => course.Lecturer)
            .Include(course => course.Attendees)
            .ThenInclude(attendance => attendance.User)
            .Include(course => course.Categories)
            .FirstOrDefault(course => course.Url == courseUrl);
    }

    public CourseEntity Insert(CourseEntity entity)
    {
        if (database.Courses.FirstOrDefault(course => course.Url == entity.Url) != null)
        {
            return null;
        }
        database.Courses.Add(entity);
        database.SaveChanges();
        return entity;
    }

    public CourseEntity Update(CourseEntity entity)
    {
        var courseToUpdate = database.Courses.Attach(entity);
        courseToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public List<CourseEntity> Search(string searchTerm)
    {
        return database.Courses.Where(c => c.Name.Contains(searchTerm) || c.Shortcut.Contains(searchTerm)).ToList();
    }
}
