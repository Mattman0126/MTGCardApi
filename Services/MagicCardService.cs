using MTGCardApi.Data;
using MTGCardApi.Models;
using MTGCardApi.Models.Cards;

namespace MTGCardApi.Services;

public class MagicCardService : IMagicCardService
{
    private readonly ICardRepository _repository;

    public MagicCardService(ICardRepository repository)
    {
        _repository = repository;
    }

    public async Task<MagicCard?> GetById(Guid id)
    {
        return await _repository.GetAsync(id, new CancellationToken());
    }

    public async Task<IEnumerable<MagicCard>> GetByName(string nameQuery)
    {
        return await _repository.GetByNameAsync(nameQuery, CancellationToken.None);

    }

    public async Task<IEnumerable<MagicCard>> GetBySetName(string setNameQuery)
    {
        return await _repository.GetBySetAsync(setNameQuery, CancellationToken.None);
    }

    // public static List<MagicCard>? GetCardsByPartialName(string query) { }

}