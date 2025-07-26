using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OutScribed.Application.Queries.DTOs.Identity;

namespace OutScribed.Infrastructure.Persistence.Reads.Configurations.Identity
{
    public class AccountAdminConfiguration : IEntityTypeConfiguration<AccountAdmin>
    {
        public void Configure(EntityTypeBuilder<AccountAdmin> builder)
        {
            builder
               .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                  .IsRequired()
                  .ValueGeneratedNever()
                  .HasMaxLength(48);

            builder
                .ToTable("AccountAdmins");


        }
    }
}
