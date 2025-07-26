using Backend.Domain.Enums;
using Backend.Domain.Models.TalesManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.TalesManagement
{
    public class TaleHistoryConfiguration : IEntityTypeConfiguration<TaleHistory>
    {
        public void Configure(EntityTypeBuilder<TaleHistory> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleHistories");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(x => x.TaleId)
         .IsRequired()
         .HasMaxLength(48);

            builder.Property(x => x.AdminId)
     .IsRequired()
     .HasMaxLength(48);

            builder
           .Property(x => x.Status)
            .HasColumnName("Status")
           .HasConversion
               (
                 v => v.ToString(),
                 v => (TaleStatuses)Enum.Parse(typeof(TaleStatuses), v)
               )
           .HasMaxLength(16);

            builder
        .OwnsOne(x => x.Reasons, x =>
        {
            x.Property(p => p.Value)
                .HasColumnName("Reasons")
                .IsRequired(false)
                .HasMaxLength(1024);

        });

        }
    }
}
