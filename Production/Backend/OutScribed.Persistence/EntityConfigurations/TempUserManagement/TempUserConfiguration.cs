using OutScribed.Domain.Models.TempUserManagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace OutScribed.Persistence.EntityConfigurations.TempUserManagement
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
                .ToTable("TempUsers");

            builder
                .Ignore(x => x.DomainEvents);


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
          .OwnsOne(x => x.Otp, x =>
          {
              x.Property(p => p.Password)
                  .HasColumnName("OtpPassword")
                  .IsRequired();

              x.Property(p => p.Date)
                 .HasColumnName("OtpDate")
                 .IsRequired();

          });


        }
    }

}
