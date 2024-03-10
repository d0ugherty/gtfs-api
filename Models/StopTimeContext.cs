using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class StopTimeContext : DbContext
{
    public StopTimeContext(DbContextOptions<StopTimeContext> options) : base(options)
    {
        // Configuration is handled by base(options)
    }

    public DbSet<StopTime> StopTimes { get; set; } = null!;
}