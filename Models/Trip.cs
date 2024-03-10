using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class Trip
{
    public int Id { get; set; }
    public string ServiceId { get; set; }
    public string TripId { get; set; }
    public string Headsign { get; set; }
    public int BlockId { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    
    [ForeignKey("Shape")]
    public int ShapeId { get; set; }
    public virtual Shape Shape { get; set; }
    
    public int DirectionId { get; set; }
    
    [ForeignKey("Route")]
    public int RouteId { get; set; }
    public virtual GtfsRoute GtfsRoute { get; set; }
}