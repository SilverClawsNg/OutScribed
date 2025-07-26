using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Discovery.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Discovery
{
    public class WatchlistConfiguration : IEntityTypeConfiguration<Watchlist>
    {
        public void Configure(EntityTypeBuilder<Watchlist> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Watchlists");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.CreatorId)
                .IsRequired();

            builder.Property(p => p.Category)
                .HasConversion<string>()
                .HasMaxLength(36)
                .IsRequired();

            builder.Property(p => p.Country)
                .HasConversion<string>()
                .HasMaxLength(48)
                .IsRequired(false);

            builder.Property(p => p.Summary)
               .HasColumnName("Summary")
               .HasMaxLength(512)
               .IsRequired();

            builder.OwnsOne(p => p.Source, navigationBuilder =>
            {
                navigationBuilder.Property(s => s.Text)
                    .HasMaxLength(28)
                    .HasColumnName("SourceText");

                navigationBuilder.Property(s => s.Url)
                    .HasMaxLength(255)
                    .HasColumnName("SourceUrl");

                navigationBuilder.WithOwner(); // Link back to watchlist
            });

            builder.HasMany(c => c.LinkedTales)
            .WithOne(c => c.Watchlist)
            .HasForeignKey(c => c.WatchlistId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Watchlist.LinkedTales))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
