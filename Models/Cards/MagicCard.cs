using System.Text.Json;
using MongoDB.Bson.Serialization.Attributes;
using MTGCardApi.Dtos;

namespace MTGCardApi.Models;

public class MagicCard
{
    //TODO: Implement factory method pattern and create the constructor
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public required Guid Id { get; set; }
    [BsonElement("name")]
    public required string Name { get; set; }
    [BsonElement("lang")]
    public required string Lang { get; set; }
    [BsonElement("releaseDate")]
    public required DateOnly ReleaseDate { get; set; }
    [BsonElement("cardUri")]
    public required Uri CardUri { get; set; }
    [BsonElement("scryfallUri")]
    public required Uri ScryfallUri { get; set; }
    [BsonElement("smallImage")]
    public required Uri? SmallImage { get; set; }
    [BsonElement("normalImage")]
    public required Uri? NormalImage { get; set; }
    [BsonElement("largeImage")]
    public required Uri? LargeImage { get; set; }
    [BsonElement("pngImage")]
    public required Uri? PngImage { get; set; }
    [BsonElement("artCropImage")]
    public required Uri? ArtCropImage { get; set; }
    [BsonElement("borderCropImage")]
    public required Uri? BorderCropImage { get; set; }
    [BsonElement("manaCost")]
    public string? ManaCost { get; set; }
    [BsonElement("cmc")]
    public double? Cmc { get; set; }
    [BsonElement("type")]
    public string? Type { get; set; }
    [BsonElement("text")]
    public string? Text { get; set; }
    [BsonElement("power")]
    public string? Power { get; set; }
    [BsonElement("toughness")]
    public string? Toughness { get; set; }
    [BsonElement("colors")]
    public string? Colors { get; set; }
    [BsonElement("colorIdentity")]
    public required string ColorIdentity { get; set; }
    [BsonElement("keywords")]
    public required string Keywords { get; set; }
    [BsonElement("legalities")]
    public required string Legalities { get; set; }
    [BsonElement("setId")]
    [BsonRepresentation(MongoDB.Bson.BsonType.String)]
    public required Guid SetId { get; set; }
    [BsonElement("setAbbr")]
    public required string SetAbbr { get; set; }
    [BsonElement("setName")]
    public required string SetName { get; set; }
    [BsonElement("setUri")]
    public required Uri SetUri { get; set; }
    [BsonElement("scryfallSetUri")]
    public required Uri ScryfallSetUri { get; set; }
    [BsonElement("rulingsUri")]
    public required Uri RulingsUri { get; set; }
    [BsonElement("printSearchUri")]
    public required Uri PrintSearchUri { get; set; }
    [BsonElement("digital")]
    public bool Digital { get; set; }
    [BsonElement("rarity")]
    public required string Rarity { get; set; }
    [BsonElement("flavorText")]
    public string? FlavorText { get; set; }
    [BsonElement("artistName")]
    public required string ArtistName { get; set; }
    [BsonElement("borderColor")]
    public required string BorderColor { get; set; }
    [BsonElement("fullArt")]
    public bool FullArt { get; set; }
    [BsonElement("textless")]
    public bool Textless { get; set; }
    [BsonElement("booster")]
    public bool Booster { get; set; }

    public void UpdateFromDto(CardDto dto)
    {
        Name = dto.Name;
        Lang = dto.Lang;
        ReleaseDate = DateOnly.Parse(dto.ReleaseDate);
        CardUri = new Uri(dto.Uri);
        ScryfallUri = new Uri(dto.ScryfallUri);
        SmallImage = dto.ImageUris?.Small != null ? new Uri(dto.ImageUris.Small) : null;
        NormalImage = dto.ImageUris?.Normal != null ? new Uri(dto.ImageUris.Normal) : null;
        LargeImage = dto.ImageUris?.Large != null ? new Uri(dto.ImageUris.Large) : null;
        PngImage = dto.ImageUris?.Png != null ? new Uri(dto.ImageUris.Png) : null;
        ArtCropImage = dto.ImageUris?.ArtCrop != null ? new Uri(dto.ImageUris.ArtCrop) : null;
        BorderCropImage = dto.ImageUris?.BorderCrop != null ? new Uri(dto.ImageUris.BorderCrop) : null;
        ManaCost = dto.ManaCost;
        Cmc = dto.Cmc;
        Type = dto.TypeLine;
        Text = dto.Text;
        Power = dto.Power;
        Toughness = dto.Toughness;
        Colors = JsonSerializer.Serialize(dto.Colors);
        ColorIdentity = JsonSerializer.Serialize(dto.ColorIdentity);
        Keywords = JsonSerializer.Serialize(dto.Keywords);
        Legalities = JsonSerializer.Serialize(dto.Legalities);
        SetId = Guid.Parse(dto.SetId);
        SetAbbr = dto.SetAbbr;
        SetName = dto.SetName;
        SetUri = new Uri(dto.SetUri);
        ScryfallSetUri = new Uri(dto.ScryfallSetUri);
        RulingsUri = new Uri(dto.RulingsUri);
        PrintSearchUri = new Uri(dto.PrintSearchUri);
        Digital = dto.Digital;
        Rarity = dto.Rarity;
        FlavorText = dto.FlavorText;
        ArtistName = dto.ArtistName;
        BorderColor = dto.BorderColor;
        FullArt = dto.FullArt;
        Textless = dto.Textless;
        Booster = dto.Booster;
    }
}