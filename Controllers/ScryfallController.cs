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
        //var path = Path.Combine(Path.GetTempPath(), "scryfallData");
        //TODO: Update to download and process file from more local directory. Use environment variable

        //Directory.CreateDirectory(path);

        var cardList = new List<CardDto>();
        await foreach (var card in _scryfallService.DownloadScryfallDataAsync())
        {
            cardList.Add(card);
        }

        //await _scryfallService.SyncCardsAsync(cardList, filePath);

        //Do I need to implement any leaf-tables to support this data? Tables containing sets/image uris/artists, etc?
        //reSeedData

        return Ok("Sync Complete");
    }

    //TODO: Add endpoint to refresh data from scryfall via their API so I can utilize pagination for improved performance.

}