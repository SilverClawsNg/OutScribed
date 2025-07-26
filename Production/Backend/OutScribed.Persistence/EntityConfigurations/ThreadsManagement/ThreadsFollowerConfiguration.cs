using OutScribed.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutScribed.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadFollowerConfiguration : IEntityTypeConfiguration<ThreadsFollower>
    {
        public void Configure(EntityTypeBuilder<ThreadsFollower> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("ThreadFollowers");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(p => p.ThreadsId)
                     .IsRequired();


            builder.Property(p => p.FollowerId)
                       .IsRequired();


            builder.HasIndex(p => new { p.ThreadsId, p.FollowerId })
               .IsUnique();
        }
    }
}
