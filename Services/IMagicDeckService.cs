using MTGCardApi.Models.Decks;

namespace MTGCardApi.Services;

public interface IMagicDeckService
{
    Task<Dictionary<Guid, MagicDeck>> GetAllAsync(CancellationToken cancellationToken);
    Task<Guid> CreateDeckEntryAsync(string name, string description, DeckFormat format, Guid? commanderId, CancellationToken cancellationToken);
    Task<MagicDeck?> AddCardById(Guid deckId, Guid cardId, int quantity, bool obtained, CancellationToken cancellationToken);
}
