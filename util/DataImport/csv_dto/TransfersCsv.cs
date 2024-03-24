namespace DataImportUtility;

public class TransfersCsv
{
	public string from_stop_id { get; set; } = null!;
	public string to_stop_id { get; set; } = null!;
	public int? transfer_type { get; set; }
	public int? min_transfer_time { get; set; }
}