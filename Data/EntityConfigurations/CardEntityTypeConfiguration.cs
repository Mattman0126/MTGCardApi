using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTGCardApi.Models;

namespace MTGCardApi.Data;

internal class CardEntityTypeConfiguration : IEntityTypeConfiguration<MagicCard>
{
    public void Configure(EntityTypeBuilder<MagicCard> builder)
    {
        builder.Property(card => card.Id).IsRequired();
        builder.Property(card => card.Name).IsRequired();
        builder.Property(card => card.Lang).IsRequired();
        builder.Property(card => card.ReleaseDate).IsRequired();
        builder.Property(card => card.CardUri).IsRequired();
        builder.Property(card => card.ScryfallUri).IsRequired();
        builder.Property(card => card.ColorIdentity).IsRequired();
        builder.Property(card => card.Keywords).IsRequired();
        builder.Property(card => card.Legalities).IsRequired();
        builder.Property(card => card.SetId).IsRequired();
        builder.Property(card => card.SetAbbr).IsRequired();
        builder.Property(card => card.SetName).IsRequired();
        builder.Property(card => card.SetUri).IsRequired();
        builder.Property(card => card.ScryfallSetUri).IsRequired();
        builder.Property(card => card.RulingsUri).IsRequired();
        builder.Property(card => card.PrintSearchUri).IsRequired();
        builder.Property(card => card.Digital).IsRequired();
        builder.Property(card => card.Rarity).IsRequired();
        builder.Property(card => card.ArtistName).IsRequired();
        builder.Property(card => card.BorderColor).IsRequired();
        builder.Property(card => card.FullArt).IsRequired();
        builder.Property(card => card.Textless).IsRequired();
        builder.Property(card => card.Booster).IsRequired();
    }
}