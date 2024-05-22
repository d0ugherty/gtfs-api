using System.ComponentModel.DataAnnotations.Schema;

namespace Gtfs.Domain.Models;

public class Agency
{
	public int Id { get; set; }
	
	public required string AgencyId { get; set; }
	public required string Name { get; set; }
	
	public string? Url { get; set; } = null!;
	public string? Timezone { get; set; } = null!;
	public string? Language { get; set; } = null!;
	public string? Email { get; set; } = null;

	public ICollection<Route> Routes = new List<Route>();
	
	[ForeignKey("Source")] 
	public int SourceId { get; set; }

	public required Source Source { get; set; }
}