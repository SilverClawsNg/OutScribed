using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Discovery;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Discovery
{
    public class WatchlistDetailConfiguration : IEntityTypeConfiguration<WatchlistDetail>
    {
        public void Configure(EntityTypeBuilder<WatchlistDetail> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("WatchlistDetails");

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
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

            builder.Property(p => p.SourceText)
                   .HasColumnName("SourceText")
                   .HasMaxLength(28)
                   .IsRequired();

            builder.Property(p => p.SourceUrl)
                     .HasColumnName("SourceUrl")
                     .HasMaxLength(255)
                     .IsRequired();

            

        }
    }
}
