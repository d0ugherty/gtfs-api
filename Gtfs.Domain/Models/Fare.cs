using System.ComponentModel.DataAnnotations.Schema;

namespace Gtfs.Domain.Models;

public class Fare
{
	public int Id { get; set; }
	public string? FareNumber { get; set; }
	public string? OriginId { get; set; }
	public string? DestinationId { get; set; }
	
	[ForeignKey("Source")]
	public int SourceId { get; set; }
	public required Source Source { get; set; }
}