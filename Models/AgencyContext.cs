using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class AgencyContext : DbContext
{
    public AgencyContext(DbContextOptions<AgencyContext> options) : base(options)
    {
        // Configuration is handled by base(options)
    }

    public DbSet<Agency> Agencies { get; set; } = null!;
}