using Microsoft.EntityFrameworkCore;
using MTGCardApi.Models;
using MTGCardApi.Models.Cards;

namespace MTGCardApi.Data;

internal class CardRepository : ICardRepository
{
    private readonly CardDbContext _dbContext;

    public CardRepository(CardDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MagicCard?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.MagicCards.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<MagicCard>> GetByNameAsync(string searchQuery, CancellationToken cancellationToken)
    {
        List<MagicCard> result = await _dbContext.MagicCards
                               .Where(card => card.Name.Contains(searchQuery) && card.Lang == "EN")
                               .OrderBy(card => card.Name)
                               .ToListAsync(cancellationToken);

        return result;
    }

    public async Task<IEnumerable<MagicCard>> GetBySetAsync(string setName, CancellationToken cancellationToken)
    {
        List<MagicCard> result = await _dbContext.MagicCards
                                .Where(card => card.SetName.Contains(setName))
                                .OrderBy(card => card.Name)
                                .ToListAsync(cancellationToken);
        return result;
    }
}