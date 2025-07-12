namespace MTGCardApi.Models.Cards;

public interface ICardRepository
{
    Task<Dictionary<Guid,MagicCard>> GetAllAsync(CancellationToken cancellationToken);
    Task<MagicCard> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Dictionary<Guid, MagicCard>> GetByIDAsync(Guid id, CancellationToken cancellationToken);
    Task<Dictionary<Guid, MagicCard>> GetByNameAsync(string nameQuery, CancellationToken cancellationToken);
    Task<Dictionary<Guid, MagicCard>> GetBySetAsync(string setName, CancellationToken cancellationToken);
    Task BulkInsertAsync(IEnumerable<MagicCard> cards);
    Task BulkUpdateAsync(IEnumerable<MagicCard> cards);
}