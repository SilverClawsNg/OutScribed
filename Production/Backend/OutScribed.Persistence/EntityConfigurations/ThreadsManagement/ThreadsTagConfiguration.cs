using OutScribed.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutScribed.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadsTagConfiguration : IEntityTypeConfiguration<ThreadsTag>
    {
        public void Configure(EntityTypeBuilder<ThreadsTag> builder)
        {
            builder
        .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder
                .ToTable("ThreadTags");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(p => p.ThreadsId)
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
