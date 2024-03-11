using System.ComponentModel.DataAnnotations;

namespace DataImportUtility;

public class StopTimesCsv
{
	public string trip_id { get; set; }
	
	[DataType(DataType.Time)]
	public DateTime arrival_time { get; set; }
    
	[DataType(DataType.Time)]
	public DateTime departure_time { get; set; }
	
	public int stop_id { get; set; }
	public int? stop_sequence { get; set; }
	public int? pickup_type { get; set; }
	public int? dropoff_type { get; set; }
}