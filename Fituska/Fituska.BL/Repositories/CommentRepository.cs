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
            //discussion.Files
            //TODO: Co všechno budeme mazat ??
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
            .Include(c => c.Files)
            .FirstOrDefault(c => c.Id == entityID);

        if (comment is not null)
            comment.SubComments = GetSubComments(entityID);

        return comment;
    }

    private ValueCollection<CommentEntity> GetSubComments(Guid parentId)
    {
        var children = database.Comments
            .Where(c => c.ParentCommentId == parentId)
            .Include(c => c.Files)
            .ToList();

        foreach (var child in children)
            child.SubComments = GetSubComments(child.Id);

        return new(children);
    }
}
