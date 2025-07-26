using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Identity.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Identity
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Profiles");

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(c => c.AccountId)
             .IsRequired();

            builder.Property(p => p.Title)
               .HasColumnName("Title")
               .HasMaxLength(128)
               .IsRequired();

            builder.Property(p => p.Bio)
               .HasColumnName("Bio")
               .HasMaxLength(512)
               .IsRequired(false);

            builder.Property(p => p.Photo)
               .HasColumnName("Photo")
               .HasMaxLength(512)
               .IsRequired(false);
        }
    }
}
