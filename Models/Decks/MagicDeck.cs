namespace MTGCardApi.Models.Decks;

public class MagicDeck
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DeckFormat Format { get; set; }
    public required List<MagicCard> Cards { get; set; }//Should this be a list of card objects or just GUID's?
    public MagicCard? Commander { get; set; }
    public bool FullyObtained { get; set; }
}
