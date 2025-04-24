using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using MTGCardApi.Models;

namespace MTGCardApi.Dtos;

public class CardDto
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }
    [JsonPropertyName("name")]
    public required string Name { get; set; }
    [JsonPropertyName("lang")]
    public required string Lang { get; set; }
    [JsonPropertyName("released_at")]
    public required string ReleaseDate { get; set; }
    [JsonPropertyName("uri")]
    public required string Uri { get; set; }
    [JsonPropertyName("scryfall_uri")]
    public required string ScryfallUri { get; set; }
    [JsonPropertyName("image_uris")]
    public ImageUrisDto? ImageUris { get; set; }
    [JsonPropertyName("mana_cost")]
    public string? ManaCost { get; set; }
    [JsonPropertyName("cmc")]
    public double? Cmc { get; set; }
    [JsonPropertyName("type_line")]
    public string? TypeLine { get; set; }
    [JsonPropertyName("oracle_text")]
    public string? Text { get; set; }
    [JsonPropertyName("power")]
    public string? Power { get; set; }
    [JsonPropertyName("toughness")]
    public string? Toughness { get; set; }
    [JsonPropertyName("colors")]
    public string[]? Colors { get; set; }
    [JsonPropertyName("color_identity")]
    public required string[] ColorIdentity { get; set; }
    [JsonPropertyName("keywords")]
    public required string[] Keywords { get; set; }
    [JsonPropertyName("legalities")]
    public JsonElement Legalities { get; set; }
    [JsonPropertyName("set_id")]
    public required string SetId { get; set; }
    [JsonPropertyName("set")]
    public required string SetAbbr { get; set; }
    [JsonPropertyName("set_name")]
    public required string SetName { get; set; }
    [JsonPropertyName("set_uri")]
    public required string SetUri { get; set; }
    [JsonPropertyName("scryfall_set_uri")]
    public required string ScryfallSetUri { get; set; }
    [JsonPropertyName("rulings_uri")]
    public required string RulingsUri { get; set; }
    [JsonPropertyName("prints_search_uri")]
    public required string PrintSearchUri { get; set; }
    [JsonPropertyName("digital")]
    public bool Digital { get; set; }
    [JsonPropertyName("rarity")]
    public required string Rarity { get; set; }
    [JsonPropertyName("flavor_text")]
    public string? FlavorText { get; set; }
    [JsonPropertyName("artist")]
    public required string ArtistName { get; set; }
    [JsonPropertyName("border_color")]
    public required string BorderColor { get; set; }
    [JsonPropertyName("full_art")]
    public bool FullArt { get; set; }
    [JsonPropertyName("textless")]
    public bool Textless { get; set; }
    [JsonPropertyName("booster")]
    public bool Booster { get; set; }

    public MagicCard ToEntity()
    {
        return new MagicCard
        {
            Id = Guid.Parse(Id),
            Name = Name,
            Lang = Lang,
            ReleaseDate = DateOnly.Parse(ReleaseDate),
            CardUri = new Uri(Uri),
            ScryfallUri = new Uri(ScryfallUri),
            SmallImage = TryUri(ImageUris?.Small),
            NormalImage = TryUri(ImageUris?.Normal),
            LargeImage = TryUri(ImageUris?.Large),
            PngImage = TryUri(ImageUris?.Png),
            ArtCropImage = TryUri(ImageUris?.ArtCrop),
            BorderCropImage = TryUri(ImageUris?.BorderCrop),
            ManaCost = ManaCost,
            Cmc = Cmc,
            Type = TypeLine,
            Text = Text,
            Power = Power,
            Toughness = Toughness,
            Colors = Colors != null ? JsonSerializer.Serialize(Colors) : null,
            ColorIdentity = JsonSerializer.Serialize(ColorIdentity),
            Keywords = JsonSerializer.Serialize(Keywords),
            Legalities = Legalities.GetRawText(),
            SetId = Guid.Parse(SetId),
            SetAbbr = SetAbbr,
            SetName = SetName,
            SetUri = new Uri(SetUri),
            ScryfallSetUri = new Uri(ScryfallSetUri),
            RulingsUri = new Uri(RulingsUri),
            PrintSearchUri = new Uri(PrintSearchUri),
            Digital = Digital,
            Rarity = Rarity,
            FlavorText = FlavorText,
            ArtistName = ArtistName,
            BorderColor = BorderColor,
            FullArt = FullArt,
            Textless = Textless,
            Booster = Booster
        };
    }

    public static Uri? TryUri(string? uri) => string.IsNullOrEmpty(uri) ? null : new Uri(uri);

    public bool EqualsEntity(MagicCard entity)
    {
        if (!Guid.TryParse(Id, out var parsedId) || entity.Id != parsedId)
        {
            return false;
        }

        return Name == entity.Name;
    }
    public bool DeepEqualsEntity(MagicCard entity)
    {
        if (!Guid.TryParse(Id, out var parsedId) || entity.Id != parsedId)
        {
            return false;
        }

        return Name == entity.Name &&
           Lang == entity.Lang &&
           DateOnly.TryParse(ReleaseDate, out var parsedDate) && entity.ReleaseDate == parsedDate &&
           Uri == entity.CardUri.ToString() &&
           ScryfallUri == entity.ScryfallUri.ToString() &&
           (ImageUris?.Small ?? "") == entity.SmallImage?.ToString() &&
           (ImageUris?.Normal ?? "") == entity.NormalImage?.ToString() &&
           (ImageUris?.Large ?? "") == entity.LargeImage?.ToString() &&
           (ImageUris?.Png ?? "") == entity.PngImage?.ToString() &&
           (ImageUris?.ArtCrop ?? "") == entity.ArtCropImage?.ToString() &&
           (ImageUris?.BorderCrop ?? "") == entity.BorderCropImage?.ToString() &&
           ManaCost == entity.ManaCost &&
           Cmc == entity.Cmc &&
           TypeLine == entity.Type &&
           Text == entity.Text &&
           Power == entity.Power &&
           Toughness == entity.Toughness &&
           JsonSerializer.Serialize(Colors) == entity.Colors &&
           JsonSerializer.Serialize(ColorIdentity) == entity.ColorIdentity &&
           JsonSerializer.Serialize(Keywords) == entity.Keywords &&
           JsonSerializer.Serialize(Legalities) == entity.Legalities &&
           Guid.TryParse(SetId, out var parsedSetId) && parsedSetId == entity.SetId &&
           SetAbbr == entity.SetAbbr &&
           SetName == entity.SetName &&
           SetUri == entity.SetUri.ToString() &&
           ScryfallSetUri == entity.ScryfallSetUri.ToString() &&
           RulingsUri == entity.RulingsUri.ToString() &&
           PrintSearchUri == entity.PrintSearchUri.ToString() &&
           Digital == entity.Digital &&
           Rarity == entity.Rarity &&
           FlavorText == entity.FlavorText &&
           ArtistName == entity.ArtistName &&
           BorderColor == entity.BorderColor &&
           FullArt == entity.FullArt &&
           Textless == entity.Textless &&
           Booster == entity.Booster;
    }
}