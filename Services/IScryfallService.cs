using MTGCardApi.Dtos;

namespace MTGCardApi.Services;

public interface IScryfallService
{
    IAsyncEnumerable<CardDto> DownloadScryfallDataAsync();
    Task SyncCardsAsync(IEnumerable<CardDto> cards);
}