using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class Transfer
{
	public int Id { get; set; }
	public string FromStopId { get; set; } = null!;
	public string ToStopId { get; set; } = null!;
	public int? TransferType { get; set; }
	public int? MinTransferTime { get; set; }

	[ForeignKey("FromStop")] 
	public int Fk_fromStopId { get; set; }

	public Stop FromStop { get; set; } = null!;

	[ForeignKey("ToStop")] 
	public int Fk_toStopId { get; set; }

	public Stop ToStop { get; set; } = null!;
}