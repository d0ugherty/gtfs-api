using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class GtfsRoute
{
    public int Id { get; set; }
    public string RouteId { get; set; }
    public string ShortName { get; set; }
    public string LongName { get; set; }
    public string? Description { get; set; }
    public int Type { get; set; }
    public string Color { get; set; }
    public string? TextColor { get; set; }
    public string? Url { get; set; }
    
    [ForeignKey("Agency")]
    public int AgencyId { get; set; }
    public virtual Agency Agency { get; set; }
}