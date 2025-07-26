using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Publishing;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Publishing
{
    public class TaleShareConfiguration : IEntityTypeConfiguration<TaleShare>
    {
        public void Configure(EntityTypeBuilder<TaleShare> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleShares");

            builder.Property(c => c.SharedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
                .IsRequired(false);

            builder.Property(c => c.TaleId)
                .IsRequired();

            builder.Property(p => p.Handle)
               .HasColumnName("Handle")
               .HasMaxLength(64)
               .IsRequired(false);

            builder.Property(p => p.Type)
               .HasConversion<string>()
               .HasMaxLength(16)
               .IsRequired();
        }
    }
}
