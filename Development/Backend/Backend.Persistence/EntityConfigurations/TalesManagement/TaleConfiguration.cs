using Backend.Domain.Enums;
using Backend.Domain.Models.TalesManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Persistence.EntityConfigurations.TalesManagement
{
    public class TaleConfiguration : IEntityTypeConfiguration<Tale>
    {
        public void Configure(EntityTypeBuilder<Tale> builder)
        {

            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("Tales");

            builder
                .Ignore(x => x.DomainEvents);

            builder
       .OwnsOne(x => x.Url, x =>
       {
           x.Property(p => p.Value)
               .HasColumnName("Url")
               .IsRequired()
               .HasMaxLength(144);

           x.HasIndex(p => p.Value).IsUnique();

       });

            builder.Property(x => x.CreatorId)
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
          .OwnsOne(x => x.Details, x =>
          {
              x.Property(p => p.Value)
                  .HasColumnName("Details")
                  .IsRequired(false)
                  .HasMaxLength(32768);

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
          .OwnsOne(x => x.Summary, x =>
          {
              x.Property(p => p.Value)
                  .HasColumnName("Summary")
                  .IsRequired(false)
                  .HasMaxLength(256);

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
            .Property(x => x.Country)
             .HasColumnName("Country")
            .HasConversion
                (
                  v => v.ToString(),
                  v => (Countries)Enum.Parse(typeof(Countries), v)
                )
            .HasMaxLength(128);

            builder
            .Property(x => x.Status)
             .HasColumnName("Status")
            .HasConversion
                (
                  v => v.ToString(),
                  v => (TaleStatuses)Enum.Parse(typeof(TaleStatuses), v)
                )
            .HasMaxLength(16);

            builder
              .HasMany(x => x.Ratings)
              .WithOne()
              .HasForeignKey(x => x.TaleId);

            builder
               .HasMany(x => x.Flags)
               .WithOne()
               .HasForeignKey(x => x.TaleId);

            builder
              .HasMany(x => x.Comments)
              .WithOne()
              .HasForeignKey(x => x.TaleId);

            builder
            .HasMany(x => x.Ratings)
            .WithOne()
            .HasForeignKey(x => x.TaleId);

            builder
            .HasMany(x => x.Followers)
            .WithOne()
            .HasForeignKey(x => x.TaleId);

            builder
          .HasMany(x => x.Sharers)
          .WithOne()
          .HasForeignKey(x => x.TaleId);

            builder
       .HasMany(x => x.Histories)
       .WithOne()
       .HasForeignKey(x => x.TaleId);

            builder
       .HasMany(x => x.Tags)
       .WithOne()
       .HasForeignKey(x => x.TaleId);

            builder.Metadata
                .FindNavigation(nameof(Tale.Histories))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
              .FindNavigation(nameof(Tale.Tags))?
              .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindNavigation(nameof(Tale.Ratings))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
                .FindNavigation(nameof(Tale.Flags))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
              .FindNavigation(nameof(Tale.Comments))?
              .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
              .FindNavigation(nameof(Tale.Followers))?
              .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
            .FindNavigation(nameof(Tale.Sharers))?
            .SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
