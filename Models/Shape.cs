namespace GtfsApi.Models;

public class Shape
{
    public int Id { get; set; }
    public int ShapeId { get; set; }
    public float ShapePtLat { get; set; }
    public float ShapePtLon { get; set; }
    public int Sequence { get; set; }
    public float? DistanceTraveled { get; set; }
}