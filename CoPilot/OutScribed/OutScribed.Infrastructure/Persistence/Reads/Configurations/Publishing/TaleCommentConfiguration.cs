using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Publishing;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Publishing
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

            builder.Property(c => c.CommentedAt)
               .IsRequired();

            builder.Property(p => p.Text)
                 .HasColumnName("Text")
                 .HasMaxLength(1024)
                 .IsRequired();

            builder.Property(c => c.UserId)
               .IsRequired();

            builder.Property(c => c.TaleId)
               .IsRequired();

            builder.HasOne(c => c.Parent)
              .WithMany(c => c.Replies)   
              .HasForeignKey(c => c.ParentId) 
              .IsRequired(false)             
              .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(TaleComment.Replies))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
