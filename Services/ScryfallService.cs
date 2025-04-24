
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MTGCardApi.Data;
using MTGCardApi.Dtos;
using MTGCardApi.Models;

namespace MTGCardApi.Services;

public class ScryfallService : IScryfallService
{
    private readonly HttpClient _httpClient;
    private readonly CardDbContext _dbContext;
    //private readonly ILogger<ScryfallService> _logger;
    // private readonly IWebHostEnvironment _env;

    public ScryfallService(HttpClient httpClient, CardDbContext dbContext)
    {
        _httpClient = httpClient;
        _dbContext = dbContext;
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("MTGCardApi/1.0");
    }

    public async Task<string> DownloadScryfallDataAsync(string targetPath)
    {
        // var response = await _httpClient.GetAsync("https://api.scryfall.com/bulk-data/default_cards");
        var request = new HttpRequestMessage(HttpMethod.Get, "https://api.scryfall.com/bulk-data/default_cards");
        var response = await _httpClient.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine($"Response: {content}");
        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"Scryfall bulk-data request failed with status code {response.StatusCode}");
        }

        using var stream = await response.Content.ReadAsStreamAsync();
        using var doc = await JsonDocument.ParseAsync(stream);

        var allCardsUrl = doc
            .RootElement
            .GetProperty("download_uri")
            .GetString();

        var fileBytes = await _httpClient.GetByteArrayAsync(allCardsUrl);
        var filePath = Path.Combine(targetPath, "AllCards.json");
        await File.WriteAllBytesAsync(filePath, fileBytes);

        return filePath;
    }

    public async IAsyncEnumerable<CardDto> StreamCardsAsync(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        using var doc = await JsonDocument.ParseAsync(stream);

        foreach (var card in doc.RootElement.EnumerateArray())
        {
            CardDto? dto = null;
            try
            {
                dto = JsonSerializer.Deserialize<CardDto>(card.GetRawText());

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Skipping card due to JSON error: {ex.Message}");
                Console.WriteLine(card.ToString());
            }
            // yield return JsonSerializer.Deserialize<CardDto>(card.GetRawText());
            if (dto is not null)
                yield return dto;
        }
    }

    public async Task SyncCardsAsync(IEnumerable<CardDto> cards, string filePath)
    {
        var existingIds = new HashSet<Guid>(await _dbContext.MagicCards.Select(c => c.Id).ToListAsync());

        var idsToUpdate = cards
            .Where(dto => Guid.TryParse(dto.Id, out var id) && existingIds.Contains(id))
            .Select(dto => Guid.Parse(dto.Id))
            .ToHashSet();

        var existingCards = await _dbContext.MagicCards
            .Where(c => idsToUpdate.Contains(c.Id))
            .ToDictionaryAsync(c => c.Id);

        var newCards = new List<MagicCard>();
        var updatedCards = new List<MagicCard>();

        foreach (var cardDto in cards)
        {
            if (!Guid.TryParse(cardDto.Id, out var parsedId))
                continue; //skip invalid Ids

            if (!existingIds.Contains(parsedId))
            {
                var newCard = cardDto.ToEntity();
                newCards.Add(newCard);
            }
            else if (existingCards.TryGetValue(parsedId, out var existingCard))
            {
                if (!cardDto.EqualsEntity(existingCard))
                {
                    existingCard.UpdateFromDto(cardDto);
                    updatedCards.Add(existingCard);
                }
            }
        }
        if (newCards.Any())
            _dbContext.MagicCards.AddRange(newCards);

        if (updatedCards.Any())
            _dbContext.MagicCards.UpdateRange(updatedCards);

        await _dbContext.SaveChangesAsync();

        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not delete file: {ex.Message}");
        }
    }
}
