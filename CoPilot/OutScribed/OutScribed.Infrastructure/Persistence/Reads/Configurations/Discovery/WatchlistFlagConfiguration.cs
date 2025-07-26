using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Discovery;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Discovery
{
    public class WatchlistFlagConfiguration : IEntityTypeConfiguration<WatchlistFlag>
    {
        public void Configure(EntityTypeBuilder<WatchlistFlag> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("WatchlistFlags");

            builder.Property(c => c.FlaggedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
              .IsRequired();

            builder.Property(c => c.WatchlistId)
              .IsRequired();

            builder.HasIndex(p => new { p.WatchlistId, p.UserId })
                .IsUnique();

            builder.Property(p => p.Type)
                 .HasConversion<string>()
                 .HasMaxLength(24)
                 .IsRequired();

        }
    }
}
