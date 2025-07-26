using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.TalesManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutScribed.Persistence.EntityConfigurations.TalesManagement
{
    public class TaleFlagConfiguration : IEntityTypeConfiguration<TaleFlag>
    {
        public void Configure(EntityTypeBuilder<TaleFlag> builder)
        {
            builder
        .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .ToTable("TaleFlags");

            builder
                .Ignore(x => x.DomainEvents);

            builder
               .Property(x => x.Type)
               .HasConversion
                   (
            v => v.ToString(),
            v => (FlagTypes)Enum.Parse(typeof(FlagTypes), v)
            )
               .HasMaxLength(16);


            builder.Property(p => p.FlaggerId)
                       .IsRequired();

            builder.Property(p => p.TaleId)
                       .IsRequired();


            builder.HasIndex(p => new { p.TaleId, p.FlaggerId })
               .IsUnique();
        }
    }
}
