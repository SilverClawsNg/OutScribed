using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Identity;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Identity
{
    public class AccountDetailConfiguration : IEntityTypeConfiguration<AccountDetail>
    {
        public void Configure(EntityTypeBuilder<AccountDetail> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("AccountDetails");

            builder.Property(c => c.RegisteredAt)
               .IsRequired();

            builder.Property(p => p.Username)
                  .HasColumnName("Username")
                  .HasMaxLength(20)
                  .IsRequired();

            builder.HasMany(c => c.Contacts)
               .WithOne()
               .HasForeignKey(c => c.AccountId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
