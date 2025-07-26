using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OutScribed.Application.Queries.DTOs.Analysis;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Analysis
{
    public class InsightListConfiguration : IEntityTypeConfiguration<InsightList>
    {
        public void Configure(EntityTypeBuilder<InsightList> builder)
        {

            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("InsightLists");

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.TaleId)
                .IsRequired();

            builder.Property(p => p.Title)
                .HasColumnName("Title")
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(p => p.Category)
              .HasConversion<string>()
              .HasMaxLength(36)
              .IsRequired();

            builder.Property(p => p.Summary)
                .HasColumnName("Summary")
                .HasMaxLength(512)
                .IsRequired();

            builder.Property(p => p.Slug)
                .HasColumnName("Slug")
                .HasMaxLength(160)
                .IsRequired();

            builder.Property(p => p.Country)
              .HasConversion<string>()
              .HasMaxLength(48)
              .IsRequired(false);

            builder.Property(p => p.Photo)
              .HasColumnName("Photo")
              .HasMaxLength(60)
              .IsRequired(false);

        }
    }
}
