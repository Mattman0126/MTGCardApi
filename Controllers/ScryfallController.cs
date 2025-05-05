using Microsoft.AspNetCore.Mvc;
using MTGCardApi.Dtos;
using MTGCardApi.Services;

namespace MTGCardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ScryfallController : ControllerBase
{
    private readonly IScryfallService _scryfallService;

    public ScryfallController(IScryfallService scryfallService)
    {
        _scryfallService = scryfallService;
    }

    [HttpPost]
    public async Task<IActionResult> RefreshCardDataFromBulkFile([FromBody] bool reSeedData)
    {
        DateTime before = DateTime.Now;
        var cardList = new List<CardDto>();
        await foreach (var card in _scryfallService.DownloadScryfallDataAsync())
        {
            cardList.Add(card);
        }

        await _scryfallService.SyncCardsAsync(cardList);

        DateTime after = DateTime.Now;

        Console.WriteLine($"Before: {before}, After: {after}");

        return Ok("Sync Complete");
    }

    //TODO: Add endpoint to refresh data from scryfall via their API so I can utilize pagination for improved performance.
}