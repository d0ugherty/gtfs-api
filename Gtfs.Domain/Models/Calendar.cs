using System.ComponentModel.DataAnnotations.Schema;

namespace Gtfs.Domain.Models;

public class Calendar
{
	public int Id { get; set; }
	public string ServiceId { get; set; } = null!;

	public int Monday { get; set; }
	public int Tuesday { get; set; }
	public int Wednesday { get; set; }
	public int Thursday { get; set; }
	public int Friday { get; set; }
	public int? Saturday { get; set; }
	public int? Sunday { get; set; }
    
	public string? StartDate { get; set; }
	public string? EndDate { get; set; }
	
	[ForeignKey("Agency")]
	public required int AgencyId { get; set; }
	public required Agency Agency { get; set; }
}