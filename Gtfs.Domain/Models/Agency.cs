using System.ComponentModel.DataAnnotations.Schema;

namespace Gtfs.Domain.Models;

public class Agency
{
	public int Id { get; set; }
	
	public required string AgencyId { get; set; }
	public required string Name { get; set; }
	
	public string? Url { get; set; }
	public string? Timezone { get; set; }
	public string? Language { get; set; }
	public string? Email { get; set; }

	public virtual List<Route> Routes { get; set; }
	
	[ForeignKey("Source")] 
	public int SourceId { get; set; }

	public Source Source { get; set; }


	public Agency()
	{
		Routes = new List<Route>();
	}
}