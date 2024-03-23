using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Models;

public class GtfsContext(DbContextOptions<GtfsContext> options) : DbContext(options)
{
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Trip>()
            .HasOne(t => t.Route)
            .WithMany() 
            .HasForeignKey(t => t.FkRouteId);
    }

    public DbSet<Agency> Agencies { get; set; } = null!;
    public DbSet<Route> Routes { get; set; } = null!;
    public DbSet<Stop> Stops { get; set; } = null!;
    public DbSet<StopTime> StopTimes { get; set; } = null!;
    public DbSet<Calendar> Calendars { get; set; } = null!;
    public DbSet<CalendarDate> CalendarDates { get; set; } = null!;
    public DbSet<Fare> Fares { get; set; } = null!;
    public DbSet<FareAttributes> FareAttributesTbl { get; set; } = null!;
    public DbSet<Shape> Shapes { get; set; } = null!;
    public DbSet<Trip> Trips { get; set; } = null!;
    public DbSet<FeedInfo> FeedInfoTbl { get; set; } = null!;
    public DbSet<Transfer> Transfers { get; set; } = null!;
    public DbSet<Mode> Modes { get; set; } = null!;

}