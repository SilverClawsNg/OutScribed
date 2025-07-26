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
    public class TaleFollowerConfiguration : IEntityTypeConfiguration<TaleFollower>
    {
        public void Configure(EntityTypeBuilder<TaleFollower> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleFollowers");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(p => p.TaleId)
                       .IsRequired();


            builder.Property(p => p.FollowerId)
                       .IsRequired();


            builder.HasIndex(p => new { p.TaleId, p.FollowerId })
               .IsUnique();
        }
    }
}
