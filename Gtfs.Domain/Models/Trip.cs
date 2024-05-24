using System.ComponentModel.DataAnnotations.Schema;

namespace Gtfs.Domain.Models;

public class Trip
{
	public int Id { get; set; }
	
	public required string ServiceId { get; set; }
	
	public required string TripId { get; set; }

	public string? Headsign { get; set; }

	public string? BlockId { get; set; }
	
	public string? ShortName { get; set; }
	public string? LongName { get; set; }
	
	public int DirectionId { get; set; }

	public int ShapeId { get; set; }

	public ICollection<StopTime> StopTimes = new List<StopTime>();
	
	public ICollection<Shape> Shapes = new List<Shape>();
	
	[ForeignKey("Route")]
	public int RouteId { get; set; }

	public required Route Route { get; set; }
	
	[ForeignKey("Source")]
	public int SourceId { get; set; }
	public required Source Source { get; set; }
}