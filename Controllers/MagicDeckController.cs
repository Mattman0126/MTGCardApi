using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MTGCardApi.Requests;
using MTGCardApi.Services;

namespace MTGCardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MagicDeckController : ControllerBase
{
    private readonly IMagicDeckService _service;
    public MagicDeckController(IMagicDeckService magicDeckService)
    {
        _service = magicDeckService;
    }
    /*
     * 
     * TODO:
     * 
     * - Update deck Endpoint
     *   - Allow updates to name, description, format, and commander (good time to implement factory pattern for aggregate roots)
     * 
     * - Delete deck endpoint
     * 
     * - Add Card(s) to deck
     * 
     * - Remove Card(s) from deck
     * 
     * */

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _service.GetAllAsync(new CancellationToken());
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeckEntryAsync([FromBody] CreateDeckRequest request, CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(DeckFormat), request.Format))
        {
            return BadRequest($"Invalid deck format: {request.Format}");
        }

        var result = await _service.CreateDeckEntryAsync(
            name: request.Name,
            description: request.Description,
            format: request.Format,
            commanderId: request.CommanderId,
            cancellationToken);

        return Created();
    }
}
