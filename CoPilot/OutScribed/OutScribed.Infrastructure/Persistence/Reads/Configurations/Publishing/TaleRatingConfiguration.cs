using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Publishing;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Publishing
{
    public class TaleRatingConfiguration : IEntityTypeConfiguration<TaleRating>
    {
        public void Configure(EntityTypeBuilder<TaleRating> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleRatings");

            builder.Property(c => c.RatedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.TaleId)
                .IsRequired();

            builder.HasIndex(p => new { p.TaleId, p.UserId })
                .IsUnique();

            builder.Property(p => p.Type)
               .HasConversion<string>()
               .HasMaxLength(16)
               .IsRequired();
        }
    }
}
