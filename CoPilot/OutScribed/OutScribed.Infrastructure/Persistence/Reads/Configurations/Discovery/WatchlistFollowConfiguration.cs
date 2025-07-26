using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Analysis;
using OutScribed.Application.Queries.DTOs.Discovery;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Discovery
{
    public class WatchlistFollowConfiguration : IEntityTypeConfiguration<WatchlistFollow>
    {
        public void Configure(EntityTypeBuilder<WatchlistFollow> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("WatchlistFollows");

            builder.Property(c => c.FollowedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.WatchlistId)
             .IsRequired();

            builder.HasIndex(p => new { p.WatchlistId, p.UserId })
             .IsUnique();

        }
    }
}
