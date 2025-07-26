using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Identity.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Identity
{
    public class LoginHistoryConfiguration : IEntityTypeConfiguration<LoginHistory>
    {
        public void Configure(EntityTypeBuilder<LoginHistory> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("LoginHistories");

            builder.Property(c => c.LoggedAt)
               .IsRequired();

            builder.Property(p => p.IpAddress)
               .HasColumnName("IpAddress")
               .HasMaxLength(45)
               .IsRequired();

            builder.Property(c => c.AccountId)
           .IsRequired();
        }
    }
}
