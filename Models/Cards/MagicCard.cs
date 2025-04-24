using System.Text.Json;
using MTGCardApi.Dtos;

namespace MTGCardApi.Models;

public class MagicCard
{
    //TODO: Implement factory method pattern and create the constructor
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Lang { get; set; }
    public required DateOnly ReleaseDate { get; set; }
    public required Uri CardUri { get; set; }
    public required Uri ScryfallUri { get; set; }
    public required Uri? SmallImage { get; set; }
    public required Uri? NormalImage { get; set; }
    public required Uri? LargeImage { get; set; }
    public required Uri? PngImage { get; set; }
    public required Uri? ArtCropImage { get; set; }
    public required Uri? BorderCropImage { get; set; }
    public string? ManaCost { get; set; }
    public double? Cmc { get; set; }
    public string? Type { get; set; }
    public string? Text { get; set; }
    public string? Power { get; set; }
    public string? Toughness { get; set; }
    public string? Colors { get; set; }
    public required string ColorIdentity { get; set; }
    public required string Keywords { get; set; }
    public required string Legalities { get; set; }
    public required Guid SetId { get; set; }
    public required string SetAbbr { get; set; }
    public required string SetName { get; set; }
    public required Uri SetUri { get; set; }
    public required Uri ScryfallSetUri { get; set; }
    public required Uri RulingsUri { get; set; }
    public required Uri PrintSearchUri { get; set; }
    public bool Digital { get; set; }
    public required string Rarity { get; set; }
    public string? FlavorText { get; set; }
    public required string ArtistName { get; set; }
    public required string BorderColor { get; set; }
    public bool FullArt { get; set; }
    public bool Textless { get; set; }
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