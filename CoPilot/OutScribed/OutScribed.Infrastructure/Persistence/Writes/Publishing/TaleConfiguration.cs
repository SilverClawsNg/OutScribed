using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Publishing.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Publishing
{
    public class TaleConfiguration : IEntityTypeConfiguration<Tale>
    {
        public void Configure(EntityTypeBuilder<Tale> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Tales");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.CreatorId)
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

            builder.HasMany(c => c.Histories)
             .WithOne(c => c.Tale)
             .HasForeignKey(c => c.Tale)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Tale.Histories))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Comments)
              .WithOne(c => c.Tale)
              .HasForeignKey(c => c.TaleId)
              .IsRequired()
              .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Tale.Comments))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Flags)
               .WithOne(c => c.Tale)
               .HasForeignKey(c => c.TaleId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Tale.Flags))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Followers)
             .WithOne(c => c.Tale)
             .HasForeignKey(c => c.TaleId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Tale.Followers))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Ratings)
             .WithOne(c => c.Tale)
             .HasForeignKey(c => c.TaleId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Tale.Ratings))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Tags)
              .WithOne(c => c.Tale)
              .HasForeignKey(c => c.TaleId)
              .IsRequired()
              .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Tale.Tags))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Shares)
             .WithOne(c => c.Tale)
             .HasForeignKey(c => c.TaleId)
             .IsRequired()
             .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Tale.Shares))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
