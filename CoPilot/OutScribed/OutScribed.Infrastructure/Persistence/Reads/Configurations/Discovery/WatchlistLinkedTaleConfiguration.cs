using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Discovery.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Discovery
{
    public class WatchlistLinkedTaleConfiguration : IEntityTypeConfiguration<LinkedTale>
    {
        public void Configure(EntityTypeBuilder<LinkedTale> builder)
        {
            builder
              .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("LinkedTales");

            builder.Property(c => c.LinkedAt)
               .IsRequired();

            builder.Property(c => c.WatchlistId)
              .IsRequired();

            builder.Property(c => c.TaleId)
             .IsRequired();


        }
    }
}
