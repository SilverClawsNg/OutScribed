using Backend.Domain.Models.UserManagement.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.EntityConfigurations.UserManagement
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
              .Property(x => x.UserId)
              .IsRequired();

            builder
                .ToTable("LoginHistories");

            builder
                .Ignore(x => x.DomainEvents);

            builder
                .Property(x => x.IpAddress)
                .HasMaxLength(28)
                .IsRequired();
        }
    }

}
