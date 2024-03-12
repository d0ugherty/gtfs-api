namespace GtfsApi.Models;

public class Agency
{
    public int Id { set; get; }
    public string AgencyId { get; set; }
    public string Name { get; set; }
    public string? Url { get; set; }
    public string Timezone { get; set; }
    public string Language { get; set; }
    public string? Email { get; set; }
}