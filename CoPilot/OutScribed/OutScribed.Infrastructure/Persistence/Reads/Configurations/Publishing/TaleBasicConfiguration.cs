using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Publishing;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Publishing
{
    public class TaleBasicConfiguration : IEntityTypeConfiguration<TaleBasic>
    {
        public void Configure(EntityTypeBuilder<TaleBasic> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleBasics");

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(p => p.Title)
              .HasColumnName("Title")
              .HasMaxLength(128)
              .IsRequired();

        }
    }
}
