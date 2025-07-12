using MTGCardApi.Dtos;
using MTGCardApi.Models;
using MTGCardApi.Models.Cards;
using MTGCardApi.Responses;
using Newtonsoft.Json;
using System.Text;

namespace MTGCardApi.Services;

public class ScryfallService : IScryfallService
{
    private readonly HttpClient _httpClient;
    private readonly ICardRepository _cardRepository;
    //private readonly ILogger<ScryfallService> _logger;
    // private readonly IWebHostEnvironment _env;

    public ScryfallService(HttpClient httpClient, ICardRepository cardRepository)
    {
        _httpClient = httpClient;
        _cardRepository = cardRepository;
        
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

        var existingCards = await _cardRepository.GetAllAsync(CancellationToken.None); 

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
            Console.WriteLine($"Cards inserted: {newCards.Count}");
            await _cardRepository.BulkInsertAsync(newCards);

        if (updatedCards.Any())
            Console.WriteLine($"Cards updated: {updatedCards.Count}");
            await _cardRepository.BulkUpdateAsync(updatedCards);
    }
}
