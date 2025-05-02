using MTGCardApi.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace MTGCardApi.Dtos;

public class CardDto
{
    [JsonProperty("id")]
    public required string Id { get; set; }//TODO: Update to guid
    [JsonProperty("name")]
    public required string Name { get; set; }
    [JsonProperty("lang")]
    public required string Lang { get; set; }
    [JsonProperty("released_at")]
    public required string ReleaseDate { get; set; }
    [JsonProperty("uri")]
    public required string Uri { get; set; }
    [JsonProperty("scryfall_uri")]
    public required string ScryfallUri { get; set; }
    [JsonProperty("image_uris")]
    public ImageUrisDto? ImageUris { get; set; }
    [JsonProperty("mana_cost")]
    public string? ManaCost { get; set; }
    [JsonProperty("cmc")]
    public double? Cmc { get; set; }
    [JsonProperty("type_line")]
    public string? TypeLine { get; set; }
    [JsonProperty("oracle_text")]
    public string? Text { get; set; }
    [JsonProperty("power")]
    public string? Power { get; set; }
    [JsonProperty("toughness")]
    public string? Toughness { get; set; }
    [JsonProperty("colors")]
    public string[]? Colors { get; set; }
    [JsonProperty("color_identity")]
    public required string[] ColorIdentity { get; set; }
    [JsonProperty("keywords")]
    public required string[] Keywords { get; set; }
    [JsonProperty("legalities")]
    public required LegalitiesDto Legalities { get; set; }
    [JsonProperty("set_id")]
    public required string SetId { get; set; }
    [JsonProperty("set")]
    public required string SetAbbr { get; set; }
    [JsonProperty("set_name")]
    public required string SetName { get; set; }
    [JsonProperty("set_uri")]
    public required string SetUri { get; set; }
    [JsonProperty("scryfall_set_uri")]
    public required string ScryfallSetUri { get; set; }
    [JsonProperty("rulings_uri")]
    public required string RulingsUri { get; set; }
    [JsonProperty("prints_search_uri")]
    public required string PrintSearchUri { get; set; }
    [JsonProperty("digital")]
    public bool Digital { get; set; }
    [JsonProperty("rarity")]
    public required string Rarity { get; set; }
    [JsonProperty("flavor_text")]
    public string? FlavorText { get; set; }
    [JsonProperty("artist")]
    public required string ArtistName { get; set; }
    [JsonProperty("border_color")]
    public required string BorderColor { get; set; }
    [JsonProperty("full_art")]
    public bool FullArt { get; set; }
    [JsonProperty("textless")]
    public bool Textless { get; set; }
    [JsonProperty("booster")]
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
            Colors = Colors != null ? JsonConvert.SerializeObject(Colors) : null,
            ColorIdentity = JsonConvert.SerializeObject(ColorIdentity),
            Keywords = JsonConvert.SerializeObject(Keywords),
            Legalities = JsonConvert.SerializeObject(Legalities),
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
           JsonConvert.SerializeObject(Colors) == entity.Colors &&
           JsonConvert.SerializeObject(ColorIdentity) == entity.ColorIdentity &&
           JsonConvert.SerializeObject(Keywords) == entity.Keywords &&
           JsonConvert.SerializeObject(Legalities) == entity.Legalities &&
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