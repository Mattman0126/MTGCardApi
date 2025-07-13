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

    public async Task<MagicCard> GetById(Guid id)
    {
        return await _repository.GetByIDAsync(id, new CancellationToken());
    }

    public async Task<Dictionary<Guid, MagicCard>> GetByName(string nameQuery)
    {
        return await _repository.GetByNameAsync(nameQuery, new CancellationToken());

    }

    public async Task<Dictionary<Guid, MagicCard>> GetBySetName(string setNameQuery)
    {
        return await _repository.GetBySetAsync(setNameQuery, new CancellationToken());
    }

    async Task<Dictionary<Guid, MagicCard>> IMagicCardService.GetAllAsync()
    {
        return await _repository.GetAllAsync(new CancellationToken());
    }

    // public static List<MagicCard>? GetCardsByPartialName(string query) { }

}