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

    public IEntity Insert(IEntity model)
    {
        var file = (FileEntity)model;
        database.Files.Add(file);
        database.SaveChanges();
        return file;
    }

    public IEntity Update(IEntity model)
    {
        var file = (FileEntity)model;
        var answerToUpdate = database.Files.Attach(file);
        answerToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return file;
    }

    public IEnumerable<IEntity> GetAll()
    {
        IEnumerable<IEntity> discussions = database.Files
            .ToList();
        return discussions;
    }
    public IEntity GetByID(Guid entityID)
    {
        FileEntity? file = database.Files.FirstOrDefault(file => file.Id == entityID);
        return file;
    }
}
