using Nemesis.Essentials.Design;

namespace Fituska.BL.Repositories;

public class CommentRepository : IRepository<CommentEntity>
{
    private readonly FituskaDbContext database;

    public CommentRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        var comment = database.Comments
            .Include(discussion => discussion.Files)
            .FirstOrDefault(discussion => discussion.Id == entityID);

        if (comment is not null)
        {
            comment.Text = "[deleted]";
            comment.UserId = Guid.Empty;
            Update(comment);
            database.SaveChanges();
        }
    }

    public CommentEntity Insert(CommentEntity entity)
    {
        database.Comments.Add(entity);
        database.SaveChanges();
        return entity;
    }

    public CommentEntity Update(CommentEntity entity)
    {
        var commentToUpdate = database.Comments.Attach(entity);
        commentToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public IEnumerable<CommentEntity> GetAll()
    {
        var comments = database.Comments
            .Include(comment => comment.Files)
            .Include(comment => comment.SubComments)
            .ToList();
        return comments;
    }

    public CommentEntity GetByID(Guid entityID)
    {
        var comment = database.Comments
            .Include(c => c.User)
            .Include(c => c.Files)
            .Include(c => c.SubComments)
            .FirstOrDefault(c => c.Id == entityID);

        return comment;
    }
}
