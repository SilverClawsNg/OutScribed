using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Analysis.Domain.Models;
using OutScribed.Modules.Discovery.Domain.Models;
using OutScribed.Modules.Publishing.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Analysis
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("tags");

            builder.Property(c => c.TaggedAt)
               .IsRequired();

            builder.Property(c => c.TagId)
                .IsRequired();

            builder.Property(c => c.InsightId)
                .IsRequired();

            builder.HasIndex(p => new { p.InsightId, p.TagId })
                .IsUnique();

        }
    }
}
