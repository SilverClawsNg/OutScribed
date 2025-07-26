using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Tagging.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Tagging
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
                .ToTable("Tags");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(c => c.TaggedAt)
               .IsRequired();

            builder.Property(p => p.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(32)
                   .IsRequired();

            builder.Property(p => p.Slug)
                  .HasColumnName("Slug")
                  .HasMaxLength(32)
                  .IsRequired();

            builder.Property(p => p.Content)
                .HasConversion<string>()
                .HasMaxLength(16)
                .IsRequired();


        }
    }
}
