using MTGCardApi.Models.Decks;

namespace MTGCardApi.Dtos.Requests;

public record CreateDeckRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required DeckFormat Format {  get; init; }
    public Guid? CommanderId { get; init; }
    public required bool FullyObtained { get; init; }
}
