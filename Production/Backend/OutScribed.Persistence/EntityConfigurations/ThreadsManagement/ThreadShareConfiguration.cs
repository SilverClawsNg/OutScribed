using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutScribed.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadShareConfiguration : IEntityTypeConfiguration<ThreadsShare>
    {
        public void Configure(EntityTypeBuilder<ThreadsShare> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("ThreadShares");

            builder
                .Ignore(x => x.DomainEvents);

            builder
         .Property(x => x.Type)
         .HasConversion
             (
               v => v.ToString(),
               v => (ContactTypes)Enum.Parse(typeof(ContactTypes), v)
             )
         .HasMaxLength(16);


            builder.Property(p => p.ThreadsId)
                       .IsRequired();


            builder.Property(p => p.SharerId)
                       .IsRequired();


            builder.HasIndex(p => new { p.ThreadsId, p.SharerId })
               .IsUnique();
        }
    }

}
