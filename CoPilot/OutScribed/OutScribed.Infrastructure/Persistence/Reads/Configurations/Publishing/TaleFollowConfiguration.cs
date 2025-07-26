using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Publishing;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Publishing
{
    public class TaleFollowConfiguration : IEntityTypeConfiguration<TaleFollow>
    {
        public void Configure(EntityTypeBuilder<TaleFollow> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleFollows");

            builder.Property(c => c.FollowedAt)
               .IsRequired();

            builder.Property(c => c.UserId)
                .IsRequired();

            builder.Property(c => c.TaleId)
             .IsRequired();

            builder.HasIndex(p => new { p.TaleId, p.UserId })
             .IsUnique();

        }
    }
}
