using Microsoft.AspNetCore.Mvc;
using MTGCardApi.Models;
using MTGCardApi.Models.Cards;
using MTGCardApi.Models.Decks;

namespace MTGCardApi.Services;

public class MagicDeckService : IMagicDeckService
{
    private readonly IDeckRepository _deckRepository;
    private readonly ICardRepository _cardRepository;
    public MagicDeckService(IDeckRepository deckRepository)
    {
        _deckRepository = deckRepository;
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
}
