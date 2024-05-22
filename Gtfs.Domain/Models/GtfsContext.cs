using Microsoft.EntityFrameworkCore;

namespace Gtfs.Domain.Models;

public class GtfsContext (DbContextOptions<GtfsContext> options) : DbContext(options)
{
	public DbSet<Source> Sources { get; set; } = null!;

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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Trip>()
			.HasOne(t => t.Route)
			.WithMany() 
			.HasForeignKey(t => t.RouteId);
		
		modelBuilder.Entity<StopTime>()
			.HasOne(st => st.Trip)
			.WithMany()
			.HasForeignKey(st => st.TripId);

		modelBuilder.Entity<Stop>()
			.HasMany(stop => stop.StopTimes)
			.WithOne(st => st.Stop)
			.HasForeignKey(st => st.StopId);

		modelBuilder.Entity<Agency>()
			.HasOne(a => a.Source)
			.WithMany()
			.HasForeignKey(a => a.SourceId);
	}
	
}