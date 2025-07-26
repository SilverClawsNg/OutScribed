using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OutScribed.Domain.Models.UserManagement.Entities;
using OutScribed.Domain.Enums;

namespace OutScribed.Persistence.EntityConfigurations.UserManagement
{
    public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> builder)
        {
            builder.Property(x => x.Id)
               .IsRequired()
               .ValueGeneratedNever()
               .HasMaxLength(48);

            builder
                .ToTable("Activities");

            builder
                .Ignore(x => x.DomainEvents);

            builder
              .Property(x => x.Type)
              .HasConversion
                  (
                    v => v.ToString(),
                    v => (ActivityTypes)Enum.Parse(typeof(ActivityTypes), v)
                  )
                   .HasColumnName("Type")
            .IsRequired()
              .HasMaxLength(32);

            builder
        .OwnsOne(x => x.Details, x =>
        {
            x.Property(p => p.Value)
                .HasColumnName("Details")
                .HasMaxLength(256);

        });

            builder
               .Property(x => x.AccountId)
               .IsRequired();


        }
    }

}
