namespace MTGCardApi.Models.Decks;

public interface IDeckRepository
{
    Task<Dictionary<Guid, MagicDeck>> GetAllAsync(CancellationToken cancellationToken);
    Task<MagicDeck?> GetByIdAsync(Guid deckId, CancellationToken cancellationToken);
    Task<Guid> CreateDeckAsync(MagicDeck magicDeck);
    Task<MagicDeck> UpdateAsync(MagicDeck magicDeck, CancellationToken cancellationToken);
}
