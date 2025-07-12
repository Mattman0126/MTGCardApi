namespace MTGCardApi.Settings;

public class MongoDBSettings
{
    public required string ConnectionString { get; set; }
    public required string DatabaseName { get; set; }
    public required string CardCollectionName { get; set; }
    public required string DeckCollectionName { get; set; }
}
