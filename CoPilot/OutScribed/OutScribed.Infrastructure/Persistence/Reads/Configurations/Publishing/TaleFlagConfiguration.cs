using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Publishing;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Publishing
{
    public class TaleFlagConfiguration : IEntityTypeConfiguration<TaleFlag>
    {
        public void Configure(EntityTypeBuilder<TaleFlag> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleFlags");

            builder.Property(c => c.FlaggedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
              .IsRequired();

            builder.Property(c => c.TaleId)
              .IsRequired();

            builder.HasIndex(p => new { p.TaleId, p.UserId })
                .IsUnique();

            builder.Property(p => p.Type)
                 .HasConversion<string>()
                 .HasMaxLength(24)
                 .IsRequired();

        }
    }
}
