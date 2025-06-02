using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTGCardApi.Models.Decks;

namespace MTGCardApi.Data.EntityConfigurations;

internal class DeckCardConfiguration : IEntityTypeConfiguration<DeckCard>
{
    public void Configure(EntityTypeBuilder<DeckCard> builder)
    {
        builder.HasKey(dc => new { dc.DeckId, dc.CardId });

        builder.Property(dc => dc.Quantity).IsRequired();
        builder.Property(dc => dc.Obtained).IsRequired();

        builder.HasOne(dc => dc.Deck)
            .WithMany(d => d.Cards)
            .HasForeignKey(dc => dc.DeckId);

        builder.HasOne(dc => dc.Card)
            .WithMany()
            .HasForeignKey(dc => dc.CardId);
    }
}
