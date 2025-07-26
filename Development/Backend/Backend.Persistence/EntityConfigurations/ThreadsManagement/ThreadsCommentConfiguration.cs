using Backend.Domain.Models.TalesManagement.Entities;
using Backend.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadsCommentConfiguration : IEntityTypeConfiguration<ThreadsComment>
    {
        public void Configure(EntityTypeBuilder<ThreadsComment> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("ThreadComments");

            builder
                .Ignore(x => x.DomainEvents);

            builder
        .OwnsOne(x => x.Details, x =>
        {
            x.Property(p => p.Value)
                .HasColumnName("Details")
                .IsRequired()
                .HasMaxLength(1024);

        });

            builder.Property(p => p.CommentatorId)
                       .IsRequired();

            builder.Property(p => p.ThreadsId)
                     .IsRequired();

            builder
                .HasMany(x => x.Ratings)
                .WithOne()
                .HasForeignKey(x => x.ThreadsCommentId);

            builder
               .HasMany(x => x.Flags)
               .WithOne()
               .HasForeignKey(x => x.ThreadsCommentId);

            builder
           .HasMany(x => x.Responses)
           .WithOne()
           .HasForeignKey(x => x.ParentId);

            builder.Metadata
                .FindNavigation(nameof(ThreadsComment.Responses))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);


            builder.Metadata
                .FindNavigation(nameof(ThreadsComment.Ratings))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindNavigation(nameof(ThreadsComment.Flags))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
