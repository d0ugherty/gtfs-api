using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class Transfer
{
	public int Id { get; set; }
	public int FromStopId { get; set; }
	public int ToStopId { get; set; }
	public int? TransferType { get; set; }
	public int? MinTransferTime { get; set; }

	[ForeignKey("Stop")] 
	public int FkFromStopId { get; set; }

	public Stop FromStop { get; set; } = null!;

	[ForeignKey("Stop")] 
	public int FkToStopId { get; set; }

	public Stop ToStop { get; set; } = null!;
}