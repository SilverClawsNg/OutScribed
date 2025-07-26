using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Analysis;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Analysis
{
    public class InsightFollowConfiguration : IEntityTypeConfiguration<InsightFollow>
    {
        public void Configure(EntityTypeBuilder<InsightFollow> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("InsightFollows");

            builder.Property(c => c.FollowedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.InsightId)
             .IsRequired();

            builder.HasIndex(p => new { p.InsightId, p.UserId })
             .IsUnique();

        }
    }
}
