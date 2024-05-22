using System.ComponentModel.DataAnnotations.Schema;

namespace Gtfs.Domain.Models;

public class Fare
{
	public int Id { get; set; }
	public string FareId { get; set; } = null!; 
	public string OriginId { get; set; } = null!;
	public string DestinationId { get; set; } = null!;
	
	[ForeignKey("FareAttributes")]
	public int FareAttributesId { get; set; }

	public FareAttributes FareAttributes { get; set; } = null!;
	
	[ForeignKey("Source")]
	public int SourceId { get; set; }
	public required Source Source { get; set; }
}