using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class Agency
{
    public int Id { set; get; }
    public string AgencyId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Url { get; set; } = null!;
    public string? Timezone { get; set; } = null!;
    public string? Language { get; set; } = null!;
    public string? Email { get; set; } = null!;
    
    [ForeignKey("ParentAgency")]
    public int Fk_parentAgencyId { get; set; }

    public ParentAgency ParentAgency { get; set; } = null!;

    //[ForeignKey("Mode")]
    //public int FkModeId { get; set; }

    // public Mode Mode { get; set; } = null!;
}