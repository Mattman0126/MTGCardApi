using FluentResults;
using MTGCardApi.Errors;
using MTGCardApi.Models;
using MTGCardApi.Models.Cards;
using MTGCardApi.Models.Decks;

namespace MTGCardApi.Services;

public class MagicDeckService : IMagicDeckService
{
    private readonly IDeckRepository _deckRepository;
    private readonly ICardRepository _cardRepository;
    public MagicDeckService(IDeckRepository deckRepository, ICardRepository cardRepository)
    {
        _deckRepository = deckRepository;
        _cardRepository = cardRepository;
    }

    public async Task<Dictionary<Guid, MagicDeck>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _deckRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Result<Guid>> CreateDeckEntryAsync(string name, string description, DeckFormat format, Guid? commanderId, CancellationToken cancellationToken)
    {
        MagicCard? commanderCard = null;

        if (commanderId.HasValue)
        {
            commanderCard = await _cardRepository.GetAsync(commanderId.Value, cancellationToken);
        }

        if (commanderCard == null)
        {
            return MagicCardError.CardNotFound(commanderId!.Value);
        }

        var deckEntry = new MagicDeck
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Format = format,
            Cards = new List<DeckCardEntry>(),
            Commander = commanderCard,
            FullyObtained = false
        };

        var result = await _deckRepository.CreateDeckAsync(deckEntry);

        return result;
    }

    public async Task<Result<MagicDeck?>> AddCardById(Guid deckId, Guid cardId, int quantity, bool obtained, CancellationToken cancellationToken)
    {
        var magicDeck = await _deckRepository.GetByIdAsync(deckId, cancellationToken);
        if (magicDeck is null)
        {
            return MagicDeckError.DeckNotFound(deckId);
        }
        
        var magicCard = await _cardRepository.GetByIDAsync(cardId, cancellationToken);
        if (magicCard is null)
        {
            return MagicCardError.CardNotFound(cardId);
        }

        DeckCardEntry deckCardEntry = new DeckCardEntry
        {
            CardId = magicCard.Id,
            Quantity = quantity,
            Obtained = obtained
        };

        if (magicDeck.Cards.Contains(deckCardEntry))
        {
            return MagicDeckError.CardAlreadyExists(deckId, cardId);
        }

        magicDeck.Cards.Add(deckCardEntry);

        var result = await _deckRepository.UpdateAsync(magicDeck, cancellationToken);

        return result;
    }
}
