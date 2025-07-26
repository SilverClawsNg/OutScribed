using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutScribed.Domain.Models.UserManagement.Entities;
using OutScribed.Domain.Enums;

namespace OutScribed.Persistence.EntityConfigurations.UserManagement
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever()
                .HasMaxLength(48);

            builder
                .ToTable("Contacts");

            builder
                .Ignore(x => x.DomainEvents);

            builder
                .Property(x => x.AccountId)
                .IsRequired();

            builder
              .Property(x => x.Type)
              .HasConversion
                  (
                    v => v.ToString(),
                    v => (ContactTypes)Enum.Parse(typeof(ContactTypes), v)
                  )
                   .HasColumnName("Type")
            .IsRequired()
              .HasMaxLength(16);

            builder
              .OwnsOne(x => x.Text, x =>
              {
                  x.Property(p => p.Value)
                      .HasColumnName("Text")
                      .IsRequired()
                      .HasMaxLength(56);

              });
        }
    }

}
