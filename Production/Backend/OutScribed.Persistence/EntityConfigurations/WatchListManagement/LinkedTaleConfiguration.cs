using OutScribed.Domain.Models.WatchListManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutScribed.Persistence.EntityConfigurations.WatchListManagement
{
    public class LinkedTaleConfiguration : IEntityTypeConfiguration<LinkedTale>
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

            builder
                .Ignore(x => x.DomainEvents);

            builder
              .Ignore(x => x.DomainEvents);

            builder.Property(p => p.WatchListId)
                       .IsRequired();


            builder.Property(p => p.TaleId)
                       .IsRequired();


            builder.HasIndex(p => new { p.WatchListId, p.TaleId })
               .IsUnique();
        }
    }

}
