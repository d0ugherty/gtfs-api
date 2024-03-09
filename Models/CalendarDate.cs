namespace GtfsApi.Models;

public class CalendarDate
{
    public int Id { get; set; }
    public int ServiceId { get; set; }
    public string Date { get; set; }
    public int ExceptionType { get; set; }
}