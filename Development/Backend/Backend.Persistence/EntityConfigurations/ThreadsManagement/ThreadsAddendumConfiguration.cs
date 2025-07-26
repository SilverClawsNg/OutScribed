using Backend.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadsAddendumConfiguration : IEntityTypeConfiguration<ThreadsAddendum>
    {
        public void Configure(EntityTypeBuilder<ThreadsAddendum> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("ThreadAddendums");

            builder
                .Ignore(x => x.DomainEvents);

            builder
        .OwnsOne(x => x.Details, x =>
        {
            x.Property(p => p.Value)
                .HasColumnName("Details")
                .IsRequired()
                .HasMaxLength(4096);

        });

            builder.Property(p => p.ThreadsId)
                     .IsRequired();

        }
    }
}
