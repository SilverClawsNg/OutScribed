using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Backend.Domain.Models.UserManagement.Entities;

namespace Backend.Persistence.EntityConfigurations.UserManagement
{
    public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
    {

        public void Configure(EntityTypeBuilder<Profile> builder)
        {

            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                     .IsRequired()
                     .ValueGeneratedNever()
                     .HasMaxLength(48);

            builder
                .ToTable("Profiles");

            builder
                .Ignore(x => x.DomainEvents);

            builder
              .Property(x => x.AccountId)
              .IsRequired();

            builder
                .OwnsOne(x => x.PhotoUrl, x =>
                {
                    x.Property(p => p.Value)
                        .HasColumnName("DisplayPhoto")
                        .IsRequired(false)
                        .HasMaxLength(60);

                });

            builder
                .OwnsOne(x => x.Title, x =>
                {
                    x.Property(p => p.Value)
                        .HasColumnName("Title")
                        .HasMaxLength(128)
                    .IsRequired(false);
                });

            builder
               .OwnsOne(x => x.Bio, x =>
               {
                   x.Property(p => p.Value)
                       .HasColumnName("Bio")
                       .HasMaxLength(512)
                       .IsRequired(false);

               });

        }

    }

}
