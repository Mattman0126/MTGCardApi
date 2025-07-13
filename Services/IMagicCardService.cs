using MTGCardApi.Models;

namespace MTGCardApi.Services;

public interface IMagicCardService
{
    Task<Dictionary<Guid, MagicCard>> GetAllAsync();
    Task<MagicCard> GetById(Guid id);
    Task<Dictionary<Guid, MagicCard>> GetByName(string nameQuery);
    Task<Dictionary<Guid, MagicCard>> GetBySetName(string setNameQuery);
}