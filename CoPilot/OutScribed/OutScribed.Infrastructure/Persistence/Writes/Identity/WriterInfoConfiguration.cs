using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Identity.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Identity
{
    public class WriterInfoConfiguration : IEntityTypeConfiguration<WriterInfo>
    {
        public void Configure(EntityTypeBuilder<WriterInfo> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("WriterInfos");

            builder.Property(c => c.AppliedAt)
               .IsRequired();

            builder.Property(c => c.ApprovedAt)
              .IsRequired(false);

            builder.Property(c => c.AccountId)
             .IsRequired();

            builder.Property(p => p.Address)
               .HasColumnName("Address")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(p => p.Application)
               .HasColumnName("Application")
               .HasMaxLength(60)
               .IsRequired();

            builder.Property(p => p.Country)
                .HasConversion<string>()
                .HasMaxLength(48)
                .IsRequired(false);
        }
    }
}
