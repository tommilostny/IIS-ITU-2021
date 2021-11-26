namespace Fituska.BL.Repositories;

/// <summary>
/// Database repository for searching.
/// </summary>
/// <typeparam name="TEntity">Database entity</typeparam>
public interface ISearchableRepository<TEntity> where TEntity : IEntity
{
    /// <summary>
    /// Search entities stored in database.
    /// </summary>
    /// <param name="searchTerm">The search term</param>
    /// <returns>Entities containing search term in their properties.</returns>
    List<TEntity> Search(string searchTerm);
}
