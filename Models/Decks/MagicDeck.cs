namespace MTGCardApi.Models.Decks;

public class MagicDeck
{
    public required Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DeckFormat Format { get; set; }
    public List<MagicCard> Cards { get; set; }
}
