namespace MTGCardApi.Models.Decks;

public interface IDeckRepository
{
    Task<Dictionary<Guid, MagicDeck>> GetAllAsync(CancellationToken cancellationToken);
    Task<Guid> CreateDeckAsync(MagicDeck magicDeck);
}
