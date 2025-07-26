using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Analysis;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Analysis
{
    public class InsightCommentConfiguration : IEntityTypeConfiguration<InsightComment>
    {
        public void Configure(EntityTypeBuilder<InsightComment> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("InsightComments");

            builder.Property(c => c.CommentedAt)
               .IsRequired();

            builder.Property(p => p.Text)
                 .HasColumnName("Text")
                 .HasMaxLength(1024)
                 .IsRequired();

            builder.Property(c => c.UserId)
               .IsRequired();

            builder.Property(c => c.InsightId)
               .IsRequired();

            builder.HasOne(c => c.Parent)
              .WithMany(c => c.Replies)   
              .HasForeignKey(c => c.ParentId) 
              .IsRequired(false)             
              .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(InsightComment.Replies))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
