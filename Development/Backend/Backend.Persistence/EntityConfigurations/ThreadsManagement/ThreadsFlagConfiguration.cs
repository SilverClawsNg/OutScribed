using Backend.Domain.Enums;
using Backend.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadsFlagConfiguration : IEntityTypeConfiguration<ThreadsFlag>
    {
        public void Configure(EntityTypeBuilder<ThreadsFlag> builder)
        {
            builder
        .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .ToTable("ThreadFlags");

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

            builder.Property(p => p.ThreadsId)
                       .IsRequired();


            builder.HasIndex(p => new { p.ThreadsId, p.FlaggerId })
               .IsUnique();
        }
    }
}
