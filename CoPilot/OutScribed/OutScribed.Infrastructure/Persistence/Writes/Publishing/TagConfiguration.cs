using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Publishing.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Publishing
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
                .ToTable("tags");

            builder.Property(c => c.TaggedAt)
               .IsRequired();

            builder.Property(c => c.TagId)
                .IsRequired();

            builder.Property(c => c.TaleId)
                .IsRequired();

            builder.HasIndex(p => new { p.TaleId, p.TagId })
                .IsUnique();

        }
    }
}
