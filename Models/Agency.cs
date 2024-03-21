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
}