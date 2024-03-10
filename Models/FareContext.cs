using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class FareContext : DbContext
{
    public FareContext(DbContextOptions<FareContext> options) : base(options)
    {
        // Configuration handled by base(options)
    }

    public DbSet<Fare> Fares { get; set; } = null!;
}