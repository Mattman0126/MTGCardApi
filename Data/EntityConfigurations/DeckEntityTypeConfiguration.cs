using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MTGCardApi.Models.Decks;

namespace MTGCardApi.Data.EntityConfigurations;

internal class DeckEntityTypeConfiguration : IEntityTypeConfiguration<MagicDeck>
{
    public void Configure(EntityTypeBuilder<MagicDeck> builder)
    {
        builder.HasKey(deck => deck.Id);

        builder.Property(deck => deck.Name).IsRequired();
        builder.Property(deck => deck.Description).IsRequired();
        builder.Property(deck => deck.Format).IsRequired();
        builder.Property(deck => deck.FullyObtained).IsRequired();

        builder.HasMany(deck => deck.Cards)
            .WithOne(dc => dc.Deck)
            .HasForeignKey(dc => dc.DeckId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(deck => deck.Commander)
            .WithMany()
            .HasForeignKey("CommanderId")
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
