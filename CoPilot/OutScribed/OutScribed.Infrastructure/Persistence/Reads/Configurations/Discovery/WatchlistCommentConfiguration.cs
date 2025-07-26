using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Discovery;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Discovery
{
    public class WatchlistCommentConfiguration : IEntityTypeConfiguration<WatchlistComment>
    {
        public void Configure(EntityTypeBuilder<WatchlistComment> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("WatchlistComments");

            builder.Property(c => c.CommentedAt)
               .IsRequired();

            builder.Property(p => p.Text)
                 .HasColumnName("Text")
                 .HasMaxLength(1024)
                 .IsRequired();

            builder.Property(c => c.UserId)
               .IsRequired();

            builder.Property(c => c.WatchlistId)
               .IsRequired();

            builder.HasOne(c => c.Parent)
              .WithMany(c => c.Replies)   
              .HasForeignKey(c => c.ParentId) 
              .IsRequired(false)             
              .OnDelete(DeleteBehavior.NoAction);

            builder.Metadata
               .FindNavigation(nameof(WatchlistComment.Replies))?
               .SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
