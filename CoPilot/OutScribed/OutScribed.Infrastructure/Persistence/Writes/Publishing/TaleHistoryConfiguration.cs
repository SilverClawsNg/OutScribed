using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Publishing.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Publishing
{
    public class TaleHistoryConfiguration : IEntityTypeConfiguration<TaleHistory>
    {
        public void Configure(EntityTypeBuilder<TaleHistory> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleHistories");

            builder.Property(c => c.HappendedAt)
               .IsRequired();

            builder.Property(c => c.AdminId)
              .IsRequired();

            builder.Property(c => c.TaleId)
            .IsRequired();

            builder.Property(p => p.Notes)
              .HasColumnName("Notes")
              .HasMaxLength(2048)
              .IsRequired(false);

            builder.Property(p => p.Status)
                .HasConversion<string>()
                .HasMaxLength(32)
                .IsRequired();
        }
    }
}
