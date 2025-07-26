using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Modules.Analysis.Domain.Models;
using OutScribed.Modules.Publishing.Domain.Models;

namespace OutScribed.Infrastructure.Persistence.Writes.Analysis
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Comments");

            builder.Property(c => c.CommentedAt)
               .IsRequired();

            builder.Property(p => p.Text)
                 .HasColumnName("Text")
                 .HasMaxLength(1024)
                 .IsRequired();

            builder.Property(c => c.CommentatorId)
               .IsRequired();

            builder.Property(c => c.InsightId)
               .IsRequired();

            builder.HasOne(c => c.Parent)
              .WithMany(c => c.Replies)   
              .HasForeignKey(c => c.ParentId) 
              .IsRequired(false)             
              .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(Comment.Replies))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
