using Microsoft.AspNetCore.Mvc;
using MTGCardApi.Dtos;
using MTGCardApi.Models;
using MTGCardApi.Services;

namespace MTGCardApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MagicCardController : ControllerBase
{
    private readonly IMagicCardService _service;
    public MagicCardController(IMagicCardService magicCardService, IScryfallService scryfallService)
    {
        _service = magicCardService;
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _service.GetAllAsync();
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCardById([FromRoute] Guid id)
    {
        var card = await _service.GetById(id);
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
        
        var card = await _service.GetByName(nameQuery);

        return Ok(card);

    }

    [HttpGet("setName/{setNameQuery}")]
    public async Task<IActionResult> GetCardBySetName([FromRoute] string setNameQuery)
    {
        if (setNameQuery == null)
        {
            return BadRequest();
        }

        var cards = await _service.GetBySetName(setNameQuery);

        return Ok(cards);
    }

    //TODO: Create the following endpoints: GetCardImages by CardID
}