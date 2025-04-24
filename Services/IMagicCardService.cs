using MTGCardApi.Models;

namespace MTGCardApi.Services;

public interface IMagicCardService
{
    Task<MagicCard?> GetById(Guid id);
    Task<IEnumerable<MagicCard>> GetByName(string nameQuery);
    Task<IEnumerable<MagicCard>> GetBySetName(string setNameQuery);
}