using OutScribed.Domain.Enums;
using OutScribed.Domain.Models.TalesManagement.Entities;
using OutScribed.Domain.Models.TempUserManagement.Entities;
using OutScribed.Domain.Models.WatchListManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Persistence.EntityConfigurations.WatchListManagement
{
    public class WatchListConfiguration : IEntityTypeConfiguration<WatchList>
    {
        public void Configure(EntityTypeBuilder<WatchList> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("WatchLists");

            builder
                .Ignore(x => x.DomainEvents);

            builder
           .OwnsOne(x => x.Source, x =>
           {
               x.Property(p => p.Url)
               .HasColumnName("SourceUrl")
                   .IsRequired()
                   .HasMaxLength(128);

               x.Property(p => p.Text)
                   .IsRequired()
                   .HasColumnName("SourceText")
                   .HasMaxLength(28);
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
   .OwnsOne(x => x.Title, x =>
   {
       x.Property(p => p.Value)
           .HasColumnName("Title")
           .IsRequired()
           .HasMaxLength(128);

   });

            builder
          .OwnsOne(x => x.Summary, x =>
          {
              x.Property(p => p.Value)
                  .HasColumnName("Summary")
                  .IsRequired()
                  .HasMaxLength(1024);

          });



            builder
            .HasMany(x => x.Followers)
            .WithOne()
            .HasForeignKey(x => x.WatchListId);

            builder
          .HasMany(x => x.Tales)
          .WithOne()
          .HasForeignKey(x => x.WatchListId);


            builder.Metadata
                .FindNavigation(nameof(WatchList.Followers))?
                .SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Metadata
              .FindNavigation(nameof(WatchList.Tales))?
              .SetPropertyAccessMode(PropertyAccessMode.Field);


        }
    }
}
