using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OutScribed.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadCommentRatingConfiguration : IEntityTypeConfiguration<ThreadsCommentRating>
    {
        public void Configure(EntityTypeBuilder<ThreadsCommentRating> builder)
        {
            builder
                 .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("ThreadCommentRatings");

            builder
                .Ignore(x => x.DomainEvents);

            builder
             .Property(x => x.Type)
             .HasConversion
                 (
                   v => v.ToString(),
                   v => (RateTypes)Enum.Parse(typeof(RateTypes), v)
                 )
             .HasMaxLength(16);


            builder.Property(p => p.ThreadsCommentId)
                       .IsRequired();


            builder.Property(p => p.RaterId)
                       .IsRequired();


            builder.HasIndex(p => new { p.ThreadsCommentId, p.RaterId })
               .IsUnique();
        }
    }
}
