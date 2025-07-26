using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Analysis.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Analysis
{
    public class ShareConfiguration : IEntityTypeConfiguration<Share>
    {
        public void Configure(EntityTypeBuilder<Share> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Shares");

            builder.Property(c => c.SharedAt)
               .IsRequired();

            builder.Property(c => c.SharerId)
                .IsRequired(false);

            builder.Property(c => c.InsightId)
                .IsRequired();

            builder.Property(p => p.Handle)
               .HasColumnName("Handle")
               .HasMaxLength(64)
               .IsRequired(false);

            builder.Property(p => p.Type)
               .HasConversion<string>()
               .HasMaxLength(16)
               .IsRequired();
        }
    }
}
