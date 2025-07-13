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

    public async Task<Guid> CreateDeckEntryAsync(string name, string description, DeckFormat format, Guid? commanderId, CancellationToken cancellationToken)
    {
        MagicCard? commanderCard = null;

        if (commanderId.HasValue)
        {
            commanderCard = await _cardRepository.GetAsync(commanderId.Value, cancellationToken);
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

    public async Task<MagicDeck?> AddCardById(Guid deckId, Guid cardId, int quantity, bool obtained, CancellationToken cancellationToken)
    {
        var magicDeck = await _deckRepository.GetByIdAsync(deckId, cancellationToken);
        if (magicDeck is null)
        {
            return null;//TODO: refactor to return a DeckNotFound exception
        }
        
        var magicCard = await _cardRepository.GetByIDAsync(cardId, cancellationToken);
        if (magicCard is null)
        {
            return null;//TODO: refactor to return a cardNotFound exception
        }

        DeckCardEntry deckCardEntry = new DeckCardEntry
        {
            CardId = magicCard.Id,
            Quantity = quantity,
            Obtained = obtained
        };

        if (magicDeck.Cards.Contains(deckCardEntry))
        {
            return null;//TODO: refactor to return a CardAlreadyInDeck exception
        }

        magicDeck.Cards.Add(deckCardEntry);

        var result = await _deckRepository.UpdateAsync(magicDeck, cancellationToken);

        return result;
    }
}
