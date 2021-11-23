using Microsoft.EntityFrameworkCore;

namespace Fituska.BL.Repositories;

public class DiscussionRepository : IRepository<DiscussionEntity>
{
    private readonly FituskaDbContext database;

    public DiscussionRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        DiscussionEntity? discussion = database.Discussions
            .Include(discussion => discussion.Files)
            .FirstOrDefault(discussion => discussion.Id == entityID);
        if (discussion != null)
        {
            discussion.Text = "Deleted";
            //discussion.Files
            //TODO: Co všechno budeme mazat ??
            Update(discussion);
            database.SaveChanges();
        }
    }

    public DiscussionEntity Insert(DiscussionEntity entity)
    {
        database.Discussions.Add(entity);
        database.SaveChanges();
        return entity;
    }

    public DiscussionEntity Update(DiscussionEntity entity)
    {
        var discussionToUpdate = database.Discussions.Attach(entity);
        discussionToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public IEnumerable<DiscussionEntity> GetAll()
    {
        IEnumerable<DiscussionEntity> discussions = database.Discussions
            .Include(discussion => discussion.Files)
            .ToList();
        return discussions;
    }
    public DiscussionEntity GetByID(Guid entityID)
    {
        DiscussionEntity? discussion = database.Discussions.FirstOrDefault(discussion => discussion.Id == entityID);
        DiscussionEntity? lastDiscussion = discussion;
        DiscussionEntity? currentDiscussion = database.Discussions.FirstOrDefault(discussion => discussion.Id == discussion.OriginId);
        while (currentDiscussion != null)
        {
            lastDiscussion.OriginDiscussion = currentDiscussion;
            lastDiscussion = currentDiscussion;
            currentDiscussion = database.Discussions.FirstOrDefault(discussion => discussion.Id == lastDiscussion.OriginId);
        }
        return discussion;
    }
}
