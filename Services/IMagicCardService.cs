using FluentResults;
using MTGCardApi.Models;

namespace MTGCardApi.Services;

public interface IMagicCardService
{
    Task<Result<Dictionary<Guid, MagicCard>>> GetAllAsync();
    Task<Result<MagicCard>> GetById(Guid id);
    Task<Result<Dictionary<Guid, MagicCard>>> GetByName(string nameQuery);
    Task<Result<Dictionary<Guid, MagicCard>>> GetBySetName(string setNameQuery);
}