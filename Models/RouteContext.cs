using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class RouteContext : DbContext
{
    public RouteContext(DbContextOptions<RouteContext> options) : base(options)
    {
        // Configuration is handled by base(options)
    }

    public DbSet<Route> Routes { get; set; } = null!;
}