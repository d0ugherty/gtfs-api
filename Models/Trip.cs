using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class Trip
{
    public int Id { get; set; }
    public string ServiceId { get; set; }
    public string TripId { get; set; }
    public string Headsign { get; set; }
    public string BlockId { get; set; }
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public int ShapeId { get; set; }
    
    public int DirectionId { get; set; }
    
    [ForeignKey("Route")]
    public int RouteId { get; set; }
    public virtual GtfsRoute Route { get; set; }
}