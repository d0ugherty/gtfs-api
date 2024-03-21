namespace GtfsApi.Models;

public class Fare
{
    public int Id { get; set; }
    public string FareId { get; set; } = null!; 
    public string OriginId { get; set; } = null!;
    public string DestinationId { get; set; } = null!;
}