using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class ShapeContext : DbContext
{
    public ShapeContext(DbContextOptions<ShapeContext> options) : base(options)
    {
        // Configuration is handled by base(options)
    }

    public DbSet<Shape> Shapes { get; set; } = null!;
}