namespace GtfsApi.Models;

public class Fare
{
    public int Id { get; set; }
    public string FareId { get; set; }
    public string OriginId { get; set; }
    public string DestinationId { get; set; }
}