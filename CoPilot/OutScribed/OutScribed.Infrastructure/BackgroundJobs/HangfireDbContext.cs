using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutScribed.Infrastructure.BackgroundJobs
{
    public class HangfireDbContext(DbContextOptions<HangfireDbContext> options)
        : DbContext(options)
    {
    }
}
