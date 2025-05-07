namespace MTGCardApi.Models.Decks;

public class MagicDeck
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DeckFormat Format { get; set; }
    public List<DeckCard> Cards { get; set; } = new();
    public MagicCard? Commander { get; set; }
    public bool FullyObtained { get; set; }
}
public class DeckCard
{
    public required Guid DeckId { get; set; }
    public required MagicDeck Deck { get; set; }
    public required Guid CardId { get; set; }
    public required MagicCard Card { get; set; } //navigation property
    public required int Quantity { get; set; }
    public bool Obtained { get; set; }
}
