using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class Route
{
    public int Id { get; set; }
    public string RouteId { get; set; } = null!;
    public string? ShortName { get; set; }
    public string? LongName { get; set; }
    public string? Description { get; set; }
    public int? Type { get; set; }
    public string? Color { get; set; }
    public string? TextColor { get; set; }
    public string? Url { get; set; }
    public string GtfsAgencyId { get; set; } = null!;
    
    
    [ForeignKey("Agency")]
    public int Fk_agencyId { get; set; }
    public virtual Agency Agency { get; set; } = null!;
    
}