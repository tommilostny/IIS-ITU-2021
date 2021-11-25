namespace Fituska.BL.Repositories;

public class CourseAttendanceRepository : IRepository<CourseAttendanceEntity>
{
    private readonly FituskaDbContext database;

    public CourseAttendanceRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        var entity = database.CourseAttendances.Find(entityID);
        if (entity is not null)
        {
            database.CourseAttendances.Remove(entity);
            database.SaveChanges();
        }
    }

    public IEnumerable<CourseAttendanceEntity> GetAll()
    {
        return database.CourseAttendances
            .Include(ca => ca.Course)
            .Include(ca => ca.User)
            .ToList();
    }

    public CourseAttendanceEntity GetByID(Guid entityID)
    {
        return database.CourseAttendances
            .Include(ca => ca.Course)
            .Include(ca => ca.User)
            .FirstOrDefault(usa => usa.Id == entityID);
    }

    public CourseAttendanceEntity Insert(CourseAttendanceEntity entity)
    {
        var entityInDb = database.CourseAttendances
            .Where(e => e.UserId == entity.UserId)
            .FirstOrDefault(e => e.CourseId == entity.CourseId);

        if (entityInDb is null)
        {
            database.CourseAttendances.Add(entity);
            database.SaveChanges();
            return entity;
        }
        return entityInDb;
    }

    public CourseAttendanceEntity Update(CourseAttendanceEntity entity)
    {
        var entityToUpdate = database.CourseAttendances.Attach(entity);
        entityToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }
}
