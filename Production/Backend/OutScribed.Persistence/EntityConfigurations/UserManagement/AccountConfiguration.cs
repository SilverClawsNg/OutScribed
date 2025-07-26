using OutScribed.Domain.Models.UserManagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Persistence.EntityConfigurations.UserManagement
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

            builder.OwnsOne(x => x.Password, x =>
            {
                x.Property(p => p.Value)
                    .HasColumnName("Password")
                    .IsRequired();

                x.Property(p => p.Salt)
                   .HasColumnName("Salt")
                   .IsRequired();
            });


            builder
                .OwnsOne(x => x.Otp, x =>
                {
                    x.Property(p => p.Password)
                        .HasColumnName("OtpValue")
                        .IsRequired();

                    x.Property(p => p.Date)
                       .HasColumnName("OtpDate")
                       .IsRequired();

                });

            builder
              .OwnsOne(x => x.RefreshToken, x =>
              {
                  x.Property(p => p.Value)
                      .HasColumnName("RefreshToken")
                      .IsRequired();

                  x.Property(p => p.ExpiryDate)
                     .HasColumnName("RefreshTokenExpiryDate")
                     .IsRequired();

              });

            builder
              .OwnsOne(x => x.Username, x =>
              {

                  x.Property(p => p.Value)
                      .HasColumnName("Username")
                      .HasMaxLength(20)
                      .IsRequired();

                  x.HasIndex(p => p.Value).IsUnique();

              });

            builder
                     .OwnsOne(x => x.EmailAddress, x =>
                     {

                         x.Property(p => p.Value)
                             .HasColumnName("EmailAddress")
                             .HasMaxLength(56)
                             .IsRequired(false);

                     });


            builder
                .OwnsOne(x => x.PhoneNumber, x =>
                {

                    x.Property(p => p.Value)
                        .HasColumnName("PhoneNumber")
                        .HasMaxLength(24)
                        .IsRequired(false);

                });


            builder
                .HasOne(x => x.Profile)
                .WithOne()
                .HasForeignKey<Profile>(e => e.AccountId)
                .IsRequired();

            builder
       .HasMany(x => x.Contacts)
       .WithOne()
       .HasForeignKey(x => x.AccountId);

            builder
     .HasMany(x => x.Activities)
     .WithOne()
     .HasForeignKey(x => x.AccountId);


            builder.Metadata
        .FindNavigation(nameof(Account.Contacts))?
        .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
     .FindNavigation(nameof(Account.Activities))?
     .SetPropertyAccessMode(PropertyAccessMode.Field);



        }
    }

}
