using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Publishing;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Publishing
{
    public class TaleDraftConfiguration : IEntityTypeConfiguration<TaleDraft>
    {
        public void Configure(EntityTypeBuilder<TaleDraft> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleDrafts");

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
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
