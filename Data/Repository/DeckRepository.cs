using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MTGCardApi.Models.Decks;
using MTGCardApi.Settings;

namespace MTGCardApi.Data.Repository;

internal class DeckRepository : IDeckRepository
{
    private readonly IMongoCollection<MagicDeck> _decks;

    public DeckRepository(IMongoDatabase db, IOptions<MongoDBSettings> settings)
    {
        _decks = db.GetCollection<MagicDeck>(settings.Value.DeckCollectionName);
    }

    public async Task<Dictionary<Guid, MagicDeck>> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _decks.Find(_ => true).ToListAsync(cancellationToken);

        return result.ToDictionary(c => c.Id);
    }

    public async Task<MagicDeck?> GetByIdAsync(Guid deckId, CancellationToken cancellationToken)
    {
        var result = await _decks.Find(c => c.Id == deckId).FirstOrDefaultAsync();

        if (result is null)
        {
            return null;
        }

        return result;
    }

    public async Task<Guid> CreateDeckAsync(MagicDeck magicDeck)
    {
        await _decks.InsertOneAsync(magicDeck);
        return magicDeck.Id;
    }

    public async Task<MagicDeck> UpdateAsync(MagicDeck magicDeck, CancellationToken cancellationToken)
    {
        await _decks.ReplaceOneAsync(deck => deck.Id == magicDeck.Id, magicDeck);
        return magicDeck;
    }
}
