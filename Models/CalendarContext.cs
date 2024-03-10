using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class CalendarContext : DbContext
{
    public CalendarContext(DbContextOptions<CalendarContext> options) : base(options)
    {
        // Configuration is handled by base(options)
    }

    public DbSet<Calendar> Calendars { get; set; } = null!;
}