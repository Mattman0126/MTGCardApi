using FluentResults;
using MTGCardApi.Errors;
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

    public async Task<Result<MagicCard>> GetById(Guid id)
    {
        var result =  await _repository.GetByIDAsync(id, new CancellationToken());

        if (result is null)
        {
            return MagicCardError.CardNotFound(id);
        }

        return result;
    }

    public async Task<Result<Dictionary<Guid, MagicCard>>> GetByName(string nameQuery)
    {
        var result = await _repository.GetByNameAsync(nameQuery, new CancellationToken());

        return result;
    }

    public async Task<Result<Dictionary<Guid, MagicCard>>> GetBySetName(string setNameQuery)
    {
        return await _repository.GetBySetAsync(setNameQuery, new CancellationToken());
    }

    public async Task<Result<Dictionary<Guid, MagicCard>>> GetAllAsync()
    {
        return await _repository.GetAllAsync(new CancellationToken());
    }
}