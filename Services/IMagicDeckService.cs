using FluentResults;
using MTGCardApi.Models.Decks;

namespace MTGCardApi.Services;

public interface IMagicDeckService
{
    Task<Dictionary<Guid, MagicDeck>> GetAllAsync(CancellationToken cancellationToken);
    Task<Result<Guid>> CreateDeckEntryAsync(string name, string description, DeckFormat format, Guid? commanderId, CancellationToken cancellationToken);
    Task<Result<MagicDeck?>> AddCardById(Guid deckId, Guid cardId, int quantity, bool obtained, CancellationToken cancellationToken);
}
