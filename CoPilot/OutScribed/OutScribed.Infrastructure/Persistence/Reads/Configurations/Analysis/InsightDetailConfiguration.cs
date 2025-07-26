using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.Modules.Analysis.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Analysis
{
    public class InsightDetailConfiguration : IEntityTypeConfiguration<InsightDetail>
    {
        public void Configure(EntityTypeBuilder<InsightDetail> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("InsightDetails");

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
                .IsRequired(false);

            builder.Property(p => p.Text)
                .HasColumnName("Text")
                .HasMaxLength(65535)
                .IsRequired(false);

            builder.Property(p => p.TaleSlug)
                .HasColumnName("TaleSlug")
                .HasMaxLength(160)
                .IsRequired();

            builder.Property(p => p.TaleTitle)
               .HasColumnName("TaleTitle")
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

            builder.HasMany(c => c.Addendums)
             .WithOne()
             .HasForeignKey(c => c.InsightId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Addendums))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Tags)
               .WithOne()
               .HasForeignKey(c => c.Id)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Tags))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }

    }
}
