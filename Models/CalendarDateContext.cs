using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class CalendarDateContext : DbContext
{
    public CalendarDateContext(DbContextOptions<CalendarDateContext> options) : base(options)
    {
        // Configuration is handled by base(options)
    }
    // Assert to the compiler that CalendarDates will not be null when accessed
    public DbSet<CalendarDate> CalendarDates { get; set; } = null!;
}