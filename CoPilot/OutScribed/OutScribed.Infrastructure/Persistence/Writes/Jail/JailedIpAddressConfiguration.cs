using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Jail.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Jail
{
    public class JailedIpAddressConfiguration : IEntityTypeConfiguration<JailedIpAddress>
    {
        public void Configure(EntityTypeBuilder<JailedIpAddress> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("JailedIpAddresses");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(c => c.JailedAt)
               .IsRequired();

            builder.Property(p => p.IpAddress)
                 .HasColumnName("IpAddress")
                 .HasMaxLength(255)
                 .IsRequired();
        }
    }
}
