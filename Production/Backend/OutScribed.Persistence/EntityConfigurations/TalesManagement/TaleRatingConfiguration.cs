using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.TalesManagement.Entities;
using OutScribed.Domain.Models.TempUserManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Persistence.EntityConfigurations.TalesManagement
{
    public class TaleRatingConfiguration : IEntityTypeConfiguration<TaleRating>
    {
        public void Configure(EntityTypeBuilder<TaleRating> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleRatings");

            builder
                .Ignore(x => x.DomainEvents);

            builder
              .Property(x => x.Type)
              .HasConversion
                  (
                    v => v.ToString(),
                    v => (RateTypes)Enum.Parse(typeof(RateTypes), v)
                  )
              .HasMaxLength(16);


            builder.Property(p => p.TaleId)
                       .IsRequired();


            builder.Property(p => p.RaterId)
                       .IsRequired();


            builder.HasIndex(p => new { p.TaleId, p.RaterId })
               .IsUnique();
        }
    }
}
