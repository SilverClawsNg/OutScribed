using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Analysis.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Analysis
{
    public class AddendumConfiguration : IEntityTypeConfiguration<Addendum>
    {
        public void Configure(EntityTypeBuilder<Addendum> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Addendums");

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.InsightId)
              .IsRequired();

            builder.Property(p => p.Text)
                .HasColumnName("Text")
                .HasMaxLength(4096)
                .IsRequired();

        }
    }
}
