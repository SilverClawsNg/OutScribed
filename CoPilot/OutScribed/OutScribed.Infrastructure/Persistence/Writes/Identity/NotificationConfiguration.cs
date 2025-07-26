using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Identity.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Identity
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Notifications");

            builder.Property(c => c.HappenedAt)
                .IsRequired();

            builder.Property(p => p.Text)
                 .HasColumnName("Text")
                 .HasMaxLength(56)
                 .IsRequired();

            builder.Property(p => p.Type)
                 .HasConversion<string>()
                 .HasMaxLength(16)
                 .IsRequired();

            builder.Property(p => p.Content)
                   .HasConversion<string>()
                   .HasMaxLength(16)
                   .IsRequired();

            builder.Property(c => c.AccountId)
                    .IsRequired();
        }
    }
}
