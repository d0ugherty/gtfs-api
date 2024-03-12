namespace DataImportUtility;

public class TripsCsv
{
	public string route_id { get; set; }
	public string service_id { get; set; }
	public string trip_id { get; set; }
	public string trip_headsign { get; set; }
	public int block_id { get; set; }
	public string? trip_short_name { get; set; }
	public int shape_id { get; set; }
	public int direction_id { get; set; }
}