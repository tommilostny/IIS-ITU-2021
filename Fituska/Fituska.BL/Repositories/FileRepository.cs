using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class FileRepository : IRepository<FileEntity>
{
    private readonly FituskaDbContext database;

    public FileRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        FileEntity? file = database.Files.Find(entityID);
        if (file != null)
        {
            database.Files.Remove(file);
            database.SaveChanges();
        }
    }

    public FileEntity Insert(FileEntity entity)
    {
        database.Files.Add(entity);
        database.SaveChanges();
        return entity;
    }

    public FileEntity Update(FileEntity entity)
    {
        var fileToUpdate = database.Files.Attach(entity);
        fileToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public IEnumerable<FileEntity> GetAll()
    {
        IEnumerable<FileEntity> discussions = database.Files
            .ToList();
        return discussions;
    }
    public FileEntity GetByID(Guid entityID)
    {
        FileEntity? file = database.Files.FirstOrDefault(file => file.Id == entityID);
        return file;
    }
}
