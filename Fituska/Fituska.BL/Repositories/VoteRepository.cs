using Microsoft.EntityFrameworkCore;

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

    public IEntity Insert(IEntity model)
    {
        VoteEntity? newVote = model as VoteEntity;
        if (newVote is null)
            throw new InvalidDataException("Missing data for converting to vote entity");
        VoteEntity? voteFromDb = database.Votes
            .Where(vote => vote.UserId == newVote.UserId)
            .FirstOrDefault(vote => vote.AnswerId == newVote.AnswerId);
        if (voteFromDb is null)
        {
            database.Votes.Add(newVote);
            database.SaveChanges();
            return newVote;
        }
        return voteFromDb;
    }

    public IEntity Update(IEntity model)
    {
        var vote = (VoteEntity)model;
        var questionToUpdate = database.Votes.Attach(vote);
        questionToUpdate.State = EntityState.Modified;
        database.SaveChanges();
        return vote;
    }

    public IEnumerable<IEntity> GetAll()
    {
        IEnumerable<IEntity> discussions = database.Votes
            .Include(vote => vote.Answer)
            .Include(vote => vote.User)
            .ToList();
        return discussions;
    }
    public IEntity GetByID(Guid entityID)
    {
        VoteEntity? vote = database.Votes
            .Include(vote => vote.Answer)
            .Include(vote => vote.User)
            .FirstOrDefault(vote => vote.Id == entityID);
        return vote!;
    }
}
