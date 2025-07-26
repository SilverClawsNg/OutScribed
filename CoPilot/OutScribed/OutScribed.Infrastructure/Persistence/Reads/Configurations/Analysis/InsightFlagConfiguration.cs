using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Analysis;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Analysis
{
    public class InsightFlagConfiguration : IEntityTypeConfiguration<InsightFlag>
    {
        public void Configure(EntityTypeBuilder<InsightFlag> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("InsightFlags");

            builder.Property(c => c.FlaggedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
              .IsRequired();

            builder.Property(c => c.InsightId)
              .IsRequired();

            builder.HasIndex(p => new { p.InsightId, p.UserId })
                .IsUnique();

            builder.Property(p => p.Type)
                 .HasConversion<string>()
                 .HasMaxLength(24)
                 .IsRequired();

        }
    }
}
