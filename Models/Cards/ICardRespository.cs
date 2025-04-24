namespace MTGCardApi.Models.Cards;

public interface ICardRepository
{
    Task<MagicCard?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MagicCard>> GetByNameAsync(string nameQuery, CancellationToken cancellationToken);
    Task<IEnumerable<MagicCard>> GetBySetAsync(string setName, CancellationToken cancellationToken);
}