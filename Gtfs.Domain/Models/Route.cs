using System.ComponentModel.DataAnnotations.Schema;

namespace Gtfs.Domain.Models;

public class Route
{
	
	public int Id;
	
	public required string RouteId { get; set; } = null!;
	
	public string? ShortName { get; set; }
	
	public string? LongName { get; set; }
	
	public string? Description { get; set; }
	
	public int? Type { get; set; }
	
	public string? Color { get; set; }
	
	public string? TextColor { get; set; }
	
	public string? Url { get; set; }

	public ICollection<Trip> Trips = new List<Trip>();

	[ForeignKey("Agency")] 
	public int AgencyId { get; set; }

	public required Agency Agency { get; set; }
}