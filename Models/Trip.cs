using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class Trip
{
    public int Id { get; set; }
    public string ServiceId { get; set; } = null!;
    public string TripId { get; set; } = null!;
    public string Headsign { get; set; } = null!;
    public string BlockId { get; set; } = null!;
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public int ShapeId { get; set; }
    public string GtfsRouteId { get; set; } = null!;
    public int DirectionId { get; set; }
    
    [ForeignKey("Route")]
    public int Fk_routeId { get; set; }

    public virtual Route Route { get; set; } = null!;
}