using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Identity.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Identity
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Accounts");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(c => c.RegisteredAt)
               .IsRequired();

            builder.Property(p => p.EmailAddress)
                 .HasColumnName("EmailAddress")
                 .HasMaxLength(255)
                 .IsRequired();

            builder.Property(p => p.Username)
                  .HasColumnName("Username")
                  .HasMaxLength(20)
                  .IsRequired();

            builder.Property(p => p.Role)
                 .HasConversion<string>()
                 .HasMaxLength(16)
                 .IsRequired();

            builder.HasMany(c => c.Contacts)
               .WithOne(c => c.Account)
               .HasForeignKey(c => c.AccountId)
               .IsRequired()
               .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Account.Contacts))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.LoginHistories)
              .WithOne(c => c.Account)
              .HasForeignKey(c => c.AccountId)
              .IsRequired()
              .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Account.LoginHistories))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(c => c.Notifications)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Account.Notifications))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsOne(p => p.Password, navigationBuilder =>
            {
                navigationBuilder.Property(s => s.Hash)
                    .HasMaxLength(28)
                    .HasColumnName("Hash");

                navigationBuilder.Property(s => s.Salt)
                    .HasMaxLength(255)
                    .HasColumnName("Salt");

                navigationBuilder.WithOwner(); 
            });

            builder.OwnsOne(p => p.RefreshToken, navigationBuilder =>
            {
                navigationBuilder.Property(s => s.Token)
                    .HasMaxLength(28)
                    .HasColumnName("RefreshToken");

                navigationBuilder.Property(s => s.ExpiresAt)
                    .HasMaxLength(255)
                    .HasColumnName("RefreshTokenExpiry");

                navigationBuilder.WithOwner();
            });

            builder.OwnsOne(p => p.OTP, navigationBuilder =>
            {
                navigationBuilder.Property(s => s.Token)
                    .HasColumnName("Token")
                    .IsRequired();

                navigationBuilder.Property(s => s.ExpiresAt)
                    .HasColumnName("TokenExpiry")
                    .IsRequired();

                navigationBuilder.WithOwner();
            });
        }
    }
}
