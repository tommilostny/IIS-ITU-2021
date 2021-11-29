namespace Fituska.BL.Repositories;

public class VoteRepository : IRepository<VoteEntity>
{
    private readonly FituskaDbContext database;

    public VoteRepository(FituskaDbContext database)
    {
        this.database = database;
    }

    public void Delete(Guid entityID)
    {
        VoteEntity? vote = database.Votes.Find(entityID);
        if (vote != null)
        {
            database.Votes.Remove(vote);
            database.SaveChanges();
        }
    }

    public VoteEntity Insert(VoteEntity entity)
    {
        VoteEntity? voteFromDb = database.Votes
            .Where(vote => vote.UserId == entity.UserId)
            .FirstOrDefault(vote => vote.AnswerId == entity.AnswerId);

        if (voteFromDb is null)
        {
            database.Votes.Add(entity);
            database.SaveChanges();
            return entity;
        }
        else
        {
            if(entity.Vote == voteFromDb.Vote)
            {
                database.Votes.Remove(voteFromDb);
                database.SaveChanges();
                return null;
            }
            else
            {
                var updatedEntity = Update(entity);
                return updatedEntity;
            }
        }
    }

    public VoteEntity Update(VoteEntity entity)
    {
        var voteToUpdate = database.Votes.Attach(entity);
        voteToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return entity;
    }

    public IEnumerable<VoteEntity> GetAll()
    {
        IEnumerable<VoteEntity> votes = database.Votes
            .Include(vote => vote.Answer)
            .Include(vote => vote.User)
            .ToList();
        return votes;
    }

    public VoteEntity GetByID(Guid entityID)
    {
        VoteEntity? vote = database.Votes
            .Include(vote => vote.Answer)
            .Include(vote => vote.User)
            .FirstOrDefault(vote => vote.Id == entityID);
        return vote!;
    }
}
