namespace DataImportUtility;

public class TransfersCsv
{
	public int from_stop_id { get; set; }
	public int to_stop_id { get; set; }
	public int? transfer_type { get; set; }
	public int? min_transfer_time { get; set; }
}