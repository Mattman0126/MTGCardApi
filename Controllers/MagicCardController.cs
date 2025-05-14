using Microsoft.AspNetCore.Mvc;
using MTGCardApi.Dtos;
using MTGCardApi.Models;
using MTGCardApi.Services;

namespace MTGCardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MagicCardController : ControllerBase
{
    private readonly IMagicCardService _magicCardService;
    public MagicCardController(IMagicCardService magicCardService, IScryfallService scryfallService)
    {
        _magicCardService = magicCardService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardById([FromRoute] Guid id)
    {
        var card = await _magicCardService.GetById(id);
        if (card == null)
        {
            return NotFound();
        }
        return Ok(card);
    }

    [HttpGet]
    public async Task<IActionResult> GetCardByName(string nameQuery)
    {
        if (nameQuery == null)
        {
            return BadRequest();
        }
        Console.WriteLine("nameQuery= " + nameQuery);
        var card = await _magicCardService.GetByName(nameQuery);

        Console.WriteLine("card= " + card.FirstOrDefault()?.Name);
        return Ok(card);

    }

    [HttpGet("setName/{setNameQuery}")]
    public async Task<IActionResult> GetCardBySetName([FromRoute] string setNameQuery)
    {
        if (setNameQuery == null)
        {
            return BadRequest();
        }
        Console.WriteLine("setNameQuery= " + setNameQuery);
        var cards = await _magicCardService.GetBySetName(setNameQuery);

        return Ok(cards);
    }

    //TODO: Create the following endpoints: GetCardImages by CardID
}