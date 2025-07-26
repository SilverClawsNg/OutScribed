using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Analysis.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Analysis
{
    public class InsightConfiguration : IEntityTypeConfiguration<Insight>
    {
        public void Configure(EntityTypeBuilder<Insight> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Insights");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.CreatorId)
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

            builder.Property(p => p.Slug)
                .HasColumnName("Slug")
                .HasMaxLength(160)
                .IsRequired(false);

            builder.Property(p => p.Country)
              .HasConversion<string>()
              .HasMaxLength(48)
              .IsRequired(false);

            builder.Property(p => p.Photo)
              .HasColumnName("Photo")
              .HasMaxLength(60)
              .IsRequired(false);

            builder.HasMany(c => c.Addendums)
             .WithOne(c => c.Insight)
             .HasForeignKey(c => c.InsightId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Addendums))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Comments)
                .WithOne(c => c.Insight)
                .HasForeignKey(c => c.InsightId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Comments))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Flags)
               .WithOne(c => c.Insight)
               .HasForeignKey(c => c.InsightId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Flags))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Followers)
             .WithOne(c => c.Insight)
             .HasForeignKey(c => c.InsightId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Followers))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Ratings)
             .WithOne(c => c.Insight)
             .HasForeignKey(c => c.InsightId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Ratings))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Tags)
              .WithOne(c => c.Insight)
              .HasForeignKey(c => c.InsightId)
              .IsRequired()
              .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Tags))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Shares)
             .WithOne(c => c.Insight)
             .HasForeignKey(c => c.InsightId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Insight.Shares))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
