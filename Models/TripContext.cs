using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class TripContext : DbContext
{
    public TripContext(DbContextOptions<TripContext> options) : base(options)
    {
        // Configuration is handled by base(options)
    }

    public DbSet<Trip> Trips { get; set; } = null!;
}