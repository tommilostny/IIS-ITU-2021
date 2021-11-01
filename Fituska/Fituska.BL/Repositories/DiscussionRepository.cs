using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;

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
        DiscussionEntity? discussion = database.Discussions.Find(entityID);
        if (discussion != null)
        {
            discussion.Text = "Deleted";
            //TODO: Co všechno budeme mazat ??
            Update(discussion);
            database.SaveChanges();
        }
    }

    public IEntity Insert(IEntity model)
    {
        var discussion = (DiscussionEntity)model;
        database.Discussions.Add(discussion);
        database.SaveChanges();
        return discussion;
    }

    public IEntity Update(IEntity model)
    {
        var discussion = (DiscussionEntity)model;
        var answerToUpdate = database.Discussions.Attach(discussion);
        answerToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return discussion;
    }

    public IEnumerable<IEntity> GetAll()
    {
        IEnumerable<IEntity> discussions= database.Discussions
            .Include(discussion => discussion.Files)
            .ToList();
        return discussions;
    }
    public IEntity GetByID(Guid entityID)
    {
        DiscussionEntity? discussion = database.Discussions.First(discussion => discussion.Id == entityID);
        DiscussionEntity? lastDiscussion = discussion;
        DiscussionEntity? currentDiscussion = database.Discussions.FirstOrDefault(discussion => discussion.Id == discussion.OriginId);
        while(currentDiscussion != null)
        {
            lastDiscussion.OriginDiscussion = currentDiscussion;
            lastDiscussion = currentDiscussion;
            currentDiscussion = database.Discussions.First(discussion => discussion.Id == lastDiscussion.OriginId);
        }
        return discussion;
    }
}
