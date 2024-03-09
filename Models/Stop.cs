namespace GtfsApi.Models;

public class Stop
{
    public int Id { get; set; }
    public string StopId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string ZoneId { get; set; }
    public string? Url { get; set; }
}