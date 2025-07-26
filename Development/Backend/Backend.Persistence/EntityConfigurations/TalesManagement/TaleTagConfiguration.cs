using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Backend.Domain.Models.TalesManagement.Entities;

namespace Backend.Persistence.EntityConfigurations.TalesManagement
{
    public class TaleTagConfiguration : IEntityTypeConfiguration<TaleTag>
    {
        public void Configure(EntityTypeBuilder<TaleTag> builder)
        {
            builder
        .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .ToTable("TaleTags");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(p => p.TaleId)
                       .IsRequired();

            builder
       .OwnsOne(x => x.Title, x =>
       {
           x.Property(p => p.Value)
               .HasColumnName("Title")
               .IsRequired()
               .HasMaxLength(32);

       });

        }
    }
}
