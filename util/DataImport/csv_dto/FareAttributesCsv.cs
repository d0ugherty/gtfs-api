namespace DataImportUtility;

public class FareAttributesCsv
{
	public string fare_id { get; set; }
	public float price { get; set; }
	public string currency_type { get; set; }
	public int? payment_method { get; set; }
	public int? transfers { get; set; }
	public int? transfer_duration { get; set; }
}