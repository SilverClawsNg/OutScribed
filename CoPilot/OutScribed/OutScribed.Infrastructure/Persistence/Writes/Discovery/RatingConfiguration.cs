using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Discovery.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Discovery
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Ratings");

            builder.Property(c => c.RatingdAt)
               .IsRequired();

            builder.Property(c => c.RaterId)
                .IsRequired();

            builder.Property(c => c.WatchlistId)
                .IsRequired();

            builder.HasIndex(p => new { p.WatchlistId, p.RaterId })
                .IsUnique();

            builder.Property(p => p.Type)
               .HasConversion<string>()
               .HasMaxLength(16)
               .IsRequired();
        }
    }
}
