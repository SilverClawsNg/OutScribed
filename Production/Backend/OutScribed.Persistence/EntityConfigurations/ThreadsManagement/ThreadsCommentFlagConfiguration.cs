using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutScribed.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadsCommentFlagConfiguration : IEntityTypeConfiguration<ThreadsCommentFlag>
    {
        public void Configure(EntityTypeBuilder<ThreadsCommentFlag> builder)
        {
            builder
       .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .ToTable("ThreadCommentFlags");

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

            builder.Property(p => p.ThreadsCommentId)
                       .IsRequired();


            builder.HasIndex(p => new { p.ThreadsCommentId, p.FlaggerId })
               .IsUnique();
        }
    }
}
