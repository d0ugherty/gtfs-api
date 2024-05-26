using Gtfs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gtfs.DataAccess {

	public class GtfsContext : DbContext
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

		public GtfsContext(DbContextOptions options) : base(options)
		{
			//Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Route>()
				.HasKey(r => r.Id);

			modelBuilder.Entity<Trip>()
				.HasKey(t => t.Id);

			modelBuilder.Entity<Shape>()
				.HasKey(s => s.Id);

			modelBuilder.Entity<Stop>()
				.HasKey(s => s.Id);

			modelBuilder.Entity<Fare>()
				.HasKey(f => f.Id);

			modelBuilder.Entity<Agency>()
				.HasKey(a => a.Id);
			
			modelBuilder.Entity<Trip>()
				.HasOne(t => t.Route)
				.WithMany(r => r.Trips)
				.HasForeignKey(t => t.RouteId);

			modelBuilder.Entity<StopTime>()
				.HasOne(st => st.Trip)
				.WithMany(t => t.StopTimes)
				.HasForeignKey(st => st.TripId);

			modelBuilder.Entity<Stop>()
				.HasMany(stop => stop.StopTimes)
				.WithOne(st => st.Stop)
				.HasForeignKey(st => st.StopId);

			modelBuilder.Entity<Agency>()
				.HasOne(a => a.Source)
				.WithMany(s => s.Agencies)
				.HasForeignKey(a => a.SourceId);
		}
	}
}