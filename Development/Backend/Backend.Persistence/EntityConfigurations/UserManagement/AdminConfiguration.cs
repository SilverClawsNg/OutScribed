using Backend.Domain.Enums;
using Backend.Domain.Models.TempUserManagement.Entities;
using Backend.Domain.Models.UserManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Persistence.EntityConfigurations.UserManagement
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Admins");

            builder
                .Ignore(x => x.DomainEvents);

            builder
        .OwnsOne(x => x.Address, x =>
        {
            x.Property(p => p.Value)
                .HasColumnName("Address")
                .IsRequired(false)
                .HasMaxLength(128);

        });

            builder
            .Property(x => x.Country)
            .HasConversion
                (
                  v => v.ToString(),
                  v => (Countries)Enum.Parse(typeof(Countries), v)
                )
                 .HasColumnName("Type")
          .IsRequired()
            .HasMaxLength(256);

            builder
    .OwnsOne(x => x.Application, x =>
    {
        x.Property(p => p.Value)
            .HasColumnName("Application")
            .IsRequired(false)
            .HasMaxLength(60);

    });
        }
    }
}
