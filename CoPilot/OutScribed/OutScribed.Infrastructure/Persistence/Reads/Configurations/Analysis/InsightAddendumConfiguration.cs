using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OutScribed.Application.Queries.DTOs.Analysis;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Analysis
{
    public class InsightAddendumConfiguration : IEntityTypeConfiguration<InsightAddendum>
    {
        public void Configure(EntityTypeBuilder<InsightAddendum> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("InsightAddendums");

            builder.Property(c => c.Date)
               .IsRequired();

            builder.Property(p => p.Text)
                .HasColumnName("Text")
                .HasMaxLength(4096)
                .IsRequired();

        }
    }

}
