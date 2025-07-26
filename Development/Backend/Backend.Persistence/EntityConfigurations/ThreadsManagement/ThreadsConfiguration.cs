using Backend.Domain.Enums;
using Backend.Domain.Models.ThreadsManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.ThreadsManagement
{
    public class ThreadsConfiguration : IEntityTypeConfiguration<Threads>
    {
        public void Configure(EntityTypeBuilder<Threads> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Threads");

            builder
                .Ignore(x => x.DomainEvents);

            builder.Property(x => x.ThreaderId)
               .IsRequired()
               .HasMaxLength(48);

            builder.Property(x => x.TaleId)
            .IsRequired()
            .HasMaxLength(48);

            builder
         .OwnsOne(x => x.Title, x =>
         {
             x.Property(p => p.Value)
                 .HasColumnName("Title")
                 .IsRequired()
                 .HasMaxLength(128);

         });

            builder
                .Property(x => x.Country)
                 .HasColumnName("Country")
                .HasConversion
                    (
                      v => v.ToString(),
                      v => (Countries)Enum.Parse(typeof(Countries), v)
                    )
                .HasMaxLength(128);

            builder
          .OwnsOne(x => x.Summary, x =>
          {
              x.Property(p => p.Value)
                  .HasColumnName("Summary")
                  .IsRequired(false)
                  .HasMaxLength(256);

          });

            builder
        .OwnsOne(x => x.PhotoUrl, x =>
        {
            x.Property(p => p.Value)
                .HasColumnName("Photo")
                .IsRequired(false)
                .HasMaxLength(60);

        });

            builder
            .Property(x => x.Category)
             .HasColumnName("Category")
            .HasConversion
                (
                  v => v.ToString(),
                  v => (Categories)Enum.Parse(typeof(Categories), v)
                )
            .HasMaxLength(48);

            builder
     .OwnsOne(x => x.Url, x =>
     {
         x.Property(p => p.Value)
             .HasColumnName("Url")
             .IsRequired()
             .HasMaxLength(144);

         x.HasIndex(p => p.Value).IsUnique();

     });

            builder
          .OwnsOne(x => x.Details, x =>
          {
              x.Property(p => p.Value)
                  .HasColumnName("Details")
                  .IsRequired(false)
                  .HasMaxLength(32768);

          });

            builder
              .HasMany(x => x.Ratings)
              .WithOne()
              .HasForeignKey(x => x.ThreadsId);

            builder
               .HasMany(x => x.Flags)
               .WithOne()
               .HasForeignKey(x => x.ThreadsId);

            builder
              .HasMany(x => x.Comments)
              .WithOne()
              .HasForeignKey(x => x.ThreadsId);

            builder
              .HasMany(x => x.Followers)
              .WithOne()
              .HasForeignKey(x => x.ThreadsId);

            builder
           .HasMany(x => x.Sharers)
           .WithOne()
           .HasForeignKey(x => x.ThreadsId);

            builder
         .HasMany(x => x.Tags)
         .WithOne()
         .HasForeignKey(x => x.ThreadsId);

            builder.Metadata
              .FindNavigation(nameof(Threads.Tags))?
              .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindNavigation(nameof(Threads.Ratings))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindNavigation(nameof(Threads.Flags))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
              .FindNavigation(nameof(Threads.Comments))?
              .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
           .FindNavigation(nameof(Threads.Followers))?
           .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
         .FindNavigation(nameof(Threads.Sharers))?
         .SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
