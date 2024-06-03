using System.ComponentModel.DataAnnotations.Schema;

namespace Gtfs.Domain.Models;

public class Shape
{
	public int Id { get; set; }
	
	public int ShapeNumber { get; set; }
	
	public float ShapePtLat { get; set; }
	
	public float ShapePtLon { get; set; }
	
	public int Sequence { get; set; }
	
	public float? DistanceTraveled { get; set; }
	
	[ForeignKey("Source")]
	public int SourceId { get; set; }
	public required Source Source { get; set; }
}