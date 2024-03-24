using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class FeedInfo
{
	public int Id { get; set; }
	public string FeedPublisherName { get; set; } = null!;
	public string FeedPublisherUrl { get; set; } = null!;
	public string? FeedLanguage { get; set; }
	public string FeedStartDate { get; set; } = null!;
	public string FeedEndDate { get; set; } = null!;
	public string FeedVersion { get; set; } = null!;
	
	[ForeignKey("Agency")]
	public int Fk_agencyId { get; set; }
	public Agency Agency { get; set; } = null!;
}
