using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Identity;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Identity
{
    public class AccountContactConfiguration : IEntityTypeConfiguration<AccountContact>
    {
        public void Configure(EntityTypeBuilder<AccountContact> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("AccountContacts");

            builder.Property(p => p.Text)
             .HasColumnName("Text")
             .HasMaxLength(56)
             .IsRequired();

            builder.Property(p => p.Type)
             .HasConversion<string>()
             .HasMaxLength(16)
             .IsRequired();

            builder.Property(c => c.AccountId)
           .IsRequired();
        }
    }
}
