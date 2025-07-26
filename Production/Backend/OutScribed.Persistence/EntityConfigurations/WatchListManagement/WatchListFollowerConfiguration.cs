using OutScribed.Domain.Models.TempUserManagement.Entities;
using OutScribed.Domain.Models.WatchListManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Persistence.EntityConfigurations.WatchListManagement
{
    public class WatchListFollowerConfiguration : IEntityTypeConfiguration<WatchListFollower>
    {
        public void Configure(EntityTypeBuilder<WatchListFollower> builder)
        {
            builder
                           .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("WatchListFollowers");

            builder
                .Ignore(x => x.DomainEvents);

            builder
              .Ignore(x => x.DomainEvents);

            builder.Property(p => p.WatchListId)
                       .IsRequired();


            builder.Property(p => p.FollowerId)
                       .IsRequired();


            builder.HasIndex(p => new { p.WatchListId, p.FollowerId })
               .IsUnique();
        }
    }
}
