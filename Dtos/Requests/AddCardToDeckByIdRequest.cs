namespace MTGCardApi.Dtos.Requests;

public record AddCardToDeckByIdRequest
{
    public required Guid MagicDeckId { get; init; }
    public required Guid MagicCardId { get; init; }
    public int Quantity { get; init; }
    public bool Obtained { get; init; }
}
