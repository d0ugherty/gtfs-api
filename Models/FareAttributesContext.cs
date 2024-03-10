using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class FareAttributesContext : DbContext
{
    public FareAttributesContext(DbContextOptions<FareAttributesContext> options) : base(options)
    {
        // Configuration handled by base(options)
    }

    public DbSet<FareAttributes> FareAttributesEnumerable { get; set; } = null!;
}