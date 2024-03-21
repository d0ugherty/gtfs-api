using System.ComponentModel.DataAnnotations;

namespace DataImportUtility;

public class StopTimesCsv
{
	public string trip_id { get; set; } = null!;
	
	[DataType(DataType.Time)]
	public string arrival_time { get; set; }
    
	[DataType(DataType.Time)]
	public string departure_time { get; set; }
	
	public int stop_id { get; set; }
	public int stop_sequence { get; set; }
	public int? pickup_type { get; set; }
	public int? drop_off_type { get; set; }
}