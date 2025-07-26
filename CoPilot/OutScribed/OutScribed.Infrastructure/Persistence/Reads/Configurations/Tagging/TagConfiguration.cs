using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Tagging;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Tagging
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
                .ToTable("TagSummaries");

            builder.Property(p => p.Name)
                 .HasColumnName("Name")
                 .HasMaxLength(32)
                 .IsRequired();

            builder.Property(p => p.Slug)
                  .HasColumnName("Slug")
                  .HasMaxLength(32)
            .IsRequired();

            builder.Property(p => p.TotalCounts)
                .HasComputedColumnSql(
                    "(\"TalesCounter\" + \"InsightsCounter\" + \"WatchlistsCounter\")",
                    stored: true 
                );

        }
    }
}
