using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MTGCardApi.Models;
using MTGCardApi.Models.Cards;
using MTGCardApi.Settings;

namespace MTGCardApi.Data;

internal class CardRepository : ICardRepository
{
    private readonly IMongoCollection<MagicCard> _cards;

    public CardRepository(IMongoDatabase db, IOptions<MongoDBSettings> settings)
    {
        _cards = db.GetCollection<MagicCard>(settings.Value.CardCollectionName);
    }

    public async Task BulkInsertAsync(IEnumerable<MagicCard> cards)
    {
        if (cards.Any())
        {
            await _cards.InsertManyAsync(cards);
        }
    }

    public async Task BulkUpdateAsync(IEnumerable<MagicCard> cards)
    {
        var updates = cards.Select(card =>
            _cards.ReplaceOneAsync(c => c.Id == card.Id, card));
        await Task.WhenAll(updates);
    }

    public async Task<Dictionary<Guid, MagicCard>> GetAllAsync(CancellationToken cancellationToken)
    {
        var count = await _cards.CountDocumentsAsync(FilterDefinition<MagicCard>.Empty);
        Console.WriteLine($"Card count: {count}");

        var cards = await _cards.Find(_ => true).ToListAsync(cancellationToken);

        return cards.ToDictionary(c => c.Id);
    }

    public async Task<MagicCard> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _cards.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);

        return result;
    }

    public async Task<MagicCard?> GetByIDAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _cards.Find(c => c.Id == id).FirstOrDefaultAsync(cancellationToken);

        if(result is null)
        {
            return null;
        }

        return result;
    }

    public async Task<Dictionary<Guid, MagicCard>> GetByNameAsync(string searchQuery, CancellationToken cancellationToken)
    {
        //List<MagicCard> result = await _dbContext.MagicCards
        //                       .Where(card => card.Name.Contains(searchQuery) && card.Lang == "EN")
        //                       .OrderBy(card => card.Name)
        //                       .ToListAsync(cancellationToken);

        //return result;
        var result = await _cards.Find(c => c.Name.Contains(searchQuery)).ToListAsync(cancellationToken);

        return result.ToDictionary(c => c.Id);
    }

    public async Task<Dictionary<Guid, MagicCard>> GetBySetAsync(string setName, CancellationToken cancellationToken)
    {
        //List<MagicCard> result = await _dbContext.MagicCards
        //                        .Where(card => card.SetName.Contains(setName))
        //                        .OrderBy(card => card.Name)
        //                        .ToListAsync(cancellationToken);
        //return result;
        throw new NotImplementedException();
    }
}