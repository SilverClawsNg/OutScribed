using Backend.Domain.Models.TalesManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.TalesManagement
{
    public class TaleCommentConfiguration : IEntityTypeConfiguration<TaleComment>
    {
        public void Configure(EntityTypeBuilder<TaleComment> builder)
        {
            builder
                .HasKey(x => x.Id);

           

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("TaleComments");

            builder
                .Ignore(x => x.DomainEvents);

            builder
            .OwnsOne(x => x.Details, x =>
            {

                x.Property(p => p.Value)
                    .HasColumnName("Details")
                    .HasMaxLength(1024)
                    .IsRequired();

            });

            builder.Property(p => p.TaleId)
                       .IsRequired();

            builder.Property(p => p.CommentatorId)
                       .IsRequired();

            builder
                .HasMany(x => x.Ratings)
                .WithOne()
                .HasForeignKey(x => x.TaleCommentId);

            builder
               .HasMany(x => x.Flags)
               .WithOne()
               .HasForeignKey(x => x.TaleCommentId);

            builder
             .HasMany(x => x.Responses)
             .WithOne()
             .HasForeignKey(x => x.ParentId);

            builder.Metadata
                .FindNavigation(nameof(TaleComment.Responses))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindNavigation(nameof(TaleComment.Ratings))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindNavigation(nameof(TaleComment.Flags))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
