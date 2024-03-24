using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace GtfsApi.Models;

public class StopTime
{
    public int Id { get; set; }

    public string ArrivalTime { get; set; } = null!;
    public string DepartureTime { get; set; } = null!;
    
    public int StopSequence { get; set; }
    public int? PickupType { get; set; }
    public int? DropoffType { get; set; }
    public string TripId { get; set; } = null!;
    public string StopId { get; set; } = null!;
    
    [ForeignKey("Stop")]
    public int Fk_stopId { get; set; }
    public virtual Stop Stop { get; set; } = null!;

    [ForeignKey("Trip")]
    public int Fk_tripId { get; set; }
    public virtual Trip Trip { get; set; } = null!;
}