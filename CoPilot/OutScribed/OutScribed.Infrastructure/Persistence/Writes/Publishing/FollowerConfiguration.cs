using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Publishing.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Publishing
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Followers");

            builder.Property(c => c.FollowedAt)
               .IsRequired();

            builder.Property(c => c.FollowerId)
                .IsRequired();

            builder.Property(c => c.TaleId)
             .IsRequired();

            builder.HasIndex(p => new { p.TaleId, p.FollowerId })
             .IsUnique();

        }
    }
}
