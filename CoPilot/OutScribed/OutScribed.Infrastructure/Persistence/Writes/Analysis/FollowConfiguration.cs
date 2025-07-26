using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Analysis.Domain.Models;


namespace OutScribed.Infrastructure.Persistence.Writes.Analysis
{
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Follows");

            builder.Property(c => c.FollowedAt)
               .IsRequired();

            builder.Property(c => c.FollowerId)
                .IsRequired();

            builder.Property(c => c.InsightId)
             .IsRequired();

            builder.HasIndex(p => new { p.InsightId, p.FollowerId })
             .IsUnique();

        }
    }
}
