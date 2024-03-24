using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class Stop
{
    public int Id { get; set; }
    public string StopId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public string? ZoneId { get; set; }
    public string? Url { get; set; }
    public string? AgencyName { get; set; }
    
    [ForeignKey("Agency")]
    public int Fk_agencyId { get; set; }

    public Agency Agency { get; set; } = null!;

}