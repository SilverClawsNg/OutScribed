using Backend.Domain.Models.UserManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.UserManagement
{
    public class AccountFollowerConfiguration : IEntityTypeConfiguration<AccountFollower>
    {
        public void Configure(EntityTypeBuilder<AccountFollower> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("AccountFollowers");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(p => p.AccountId)
                       .IsRequired();

            builder.Property(p => p.FollowerId)
                       .IsRequired();

            builder.HasIndex(p => new { p.AccountId, p.FollowerId })
               .IsUnique();
        }
    }
}
