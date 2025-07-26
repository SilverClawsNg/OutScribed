using Microsoft.EntityFrameworkCore;

namespace Backend.Persistence.EntityConfigurations
{
    public class HangfireDbContext(DbContextOptions<HangfireDbContext> options)
        : DbContext(options)
    {
    }
}
