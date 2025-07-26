using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Analysis.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Analysis
{
    public class FlagConfiguration : IEntityTypeConfiguration<Flag>
    {
        public void Configure(EntityTypeBuilder<Flag> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Flags");

            builder.Property(c => c.FlaggedAt)
               .IsRequired();

            builder.Property(c => c.FlaggerId)
              .IsRequired();

            builder.Property(c => c.InsightId)
              .IsRequired();

            builder.HasIndex(p => new { p.InsightId, p.FlaggerId })
                .IsUnique();

            builder.Property(p => p.Type)
                 .HasConversion<string>()
                 .HasMaxLength(24)
                 .IsRequired();

        }
    }
}
