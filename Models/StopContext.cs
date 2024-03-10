using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class StopContext : DbContext
{
    public StopContext(DbContextOptions<StopContext> options) : base(options)
    {
        // Configuration is handled by base(options)
    }

    public DbSet<Stop> Stops { get; set; } = null!;
}