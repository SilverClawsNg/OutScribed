using Microsoft.EntityFrameworkCore;

namespace OutScribed.Persistence.EntityConfigurations
{
    public class HangfireDbContext(DbContextOptions<HangfireDbContext> options)
        : DbContext(options)
    {
    }
}
