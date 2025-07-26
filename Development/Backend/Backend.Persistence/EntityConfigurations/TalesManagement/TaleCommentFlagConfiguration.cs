using Backend.Domain.Enums;
using Backend.Domain.Models.TalesManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.TalesManagement
{
    public class TaleCommentFlagConfiguration : IEntityTypeConfiguration<TaleCommentFlag>
    {
        public void Configure(EntityTypeBuilder<TaleCommentFlag> builder)
        {
            builder
        .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .ToTable("TaleCommentFlags");

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

            builder.Property(p => p.TaleCommentId)
                       .IsRequired();


            builder.HasIndex(p => new { p.TaleCommentId, p.FlaggerId })
               .IsUnique();
        }
    }
}
