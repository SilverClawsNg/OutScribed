using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Onboarding.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Onboarding
{
    public class TempUserConfiguration : IEntityTypeConfiguration<TempUser>
    {
        public void Configure(EntityTypeBuilder<TempUser> builder)
        {
            builder
             .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("tempusers");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(c => c.CreatedAt)
               .IsRequired();

            builder.Property(p => p.EmailAddress)
                 .HasColumnName("EmailAddress")
                 .HasMaxLength(255)
                 .IsRequired();

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
