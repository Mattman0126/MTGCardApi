using MTGCardApi.Models;
using MTGCardApi.Models.Decks;

namespace MTGCardApi.Errors;

public class MagicDeckError : BaseError
{
    public MagicDeckError(Guid magicDeckId, ErrorType errorType, string message) : base(errorType, message)
    {
        MagicDeckId = magicDeckId;
        Metadata.Add(nameof(magicDeckId), magicDeckId);
    }
   
    public Guid MagicDeckId { get; }

    public static MagicDeckError CardAlreadyExists(Guid magicDeckId, Guid cardId)
    {
        return new MagicDeckError(magicDeckId, ErrorType.AlreadyExists, $"Record already exists. {nameof(MagicCard)} with ID of {cardId} already exists in magic deck ID {magicDeckId}.");
    }

    public static MagicDeckError DeckNotFound(Guid magicDeckId)
    {
        return new MagicDeckError(magicDeckId, ErrorType.DoesNotExist, $"{nameof(MagicDeck)} was not found.");
    }


}
