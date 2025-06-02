using Microsoft.EntityFrameworkCore;
using MTGCardApi.Data.EntityConfigurations;
using MTGCardApi.Models;

namespace MTGCardApi.Data;

public class CardDbContext : DbContext
{
    public DbSet<MagicCard> MagicCards { get; set; }

    public CardDbContext() { }
    public CardDbContext(DbContextOptions<CardDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DeckEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DeckCardConfiguration());
    }
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        _ = await SaveChangesAsync(cancellationToken);
        return true;
    }
}