using MongoDB.Bson.Serialization.Attributes;

namespace MTGCardApi.Models.Decks;

public class MagicDeck
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public required Guid Id { get; set; }
    [BsonElement("name")]
    public required string Name { get; set; }
    [BsonElement("description")]
    public required string Description { get; set; }
    [BsonElement("format")]
    public required DeckFormat Format { get; set; }
    [BsonElement("cards")]
    public List<DeckCardEntry> Cards { get; set; } = new();
    [BsonElement("commander")]
    public MagicCard? Commander { get; set; }
    [BsonElement("fullyObtained")]
    public bool FullyObtained { get; set; }


}
public class DeckCardEntry
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public required Guid CardId { get; set; }
    [BsonElement("quantity")]
    public required int Quantity { get; set; }
    [BsonElement("obtained")]
    public bool Obtained { get; set; }
}
