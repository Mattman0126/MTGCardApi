using MTGCardApi.Dtos;

namespace MTGCardApi.Services;

public interface IScryfallService
{
    Task<string> DownloadScryfallDataAsync(string targetPath);
    IAsyncEnumerable<CardDto> StreamCardsAsync(string filePath);
    Task SyncCardsAsync(IEnumerable<CardDto> cards, string filePath);
}