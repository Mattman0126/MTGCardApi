
using Microsoft.EntityFrameworkCore;
using MTGCardApi.Data;
using MTGCardApi.Dtos;
using MTGCardApi.Models;
using MTGCardApi.Responses;
using Newtonsoft.Json;
using System.Text;

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
        
    }

    //TODO: Look into creating a background service for this process rather than having its own endpoint

    public async IAsyncEnumerable<CardDto> DownloadScryfallDataAsync()
    {
        //get object with download url
        var response = await _httpClient.GetAsync("https://api.scryfall.com/bulk-data/default_cards");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();
        
        //deserialize the download url
        var scryfallData = JsonConvert.DeserializeObject<ScryfallBulkDataResponse>(content);        

        //download the file from scryfall
        var fileBytes = await _httpClient.GetByteArrayAsync(scryfallData!.DownloadUri);

        //convert byte array to json string
        var jsonString = Encoding.UTF8.GetString(fileBytes);

        var cards = JsonConvert.DeserializeObject<List<CardDto>>(jsonString);

        //deserialize json into dto objects
        if (cards is not null)
        {
            foreach (var card in cards)
            {
                yield return card;
            }
        }
    }

    public async Task SyncCardsAsync(IEnumerable<CardDto> cards)
    {
        //TODO: Update to compare and update/insert records X at a time OR look into utilizing efcore bulkextensions

        //Get all cards from DB for comparisons
        var existingIds = new HashSet<Guid>(await _dbContext.MagicCards.Select(c => c.Id).ToListAsync());

        //identify cards needing updates
        var existingCards = await _dbContext.MagicCards
            .Where(c => existingIds.Contains(c.Id))
            .ToDictionaryAsync(c => c.Id);

        var newCards = new List<MagicCard>();
        var updatedCards = new List<MagicCard>();

        foreach (var cardDto in cards)
        {
            if (!Guid.TryParse(cardDto.Id, out var parsedId))
                continue; //skip invalid Ids

            if (existingCards.TryGetValue(parsedId, out var existingCard))
            {
                if (!cardDto.EqualsEntity(existingCard))
                {
                    existingCard.UpdateFromDto(cardDto);
                    updatedCards.Add(existingCard);
                }
            }
            else
            {
                newCards.Add(cardDto.ToEntity());
            }
        }
        if (newCards.Any())
            _dbContext.MagicCards.AddRange(newCards);

        if (updatedCards.Any())
            _dbContext.MagicCards.UpdateRange(updatedCards);

        await _dbContext.SaveChangesAsync();

    }
}
