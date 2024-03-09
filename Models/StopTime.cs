using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace GtfsApi.Models;

public class StopTime
{
    public int Id { get; set; }
    
    [DataType(DataType.Time)]
    public DateTime ArrivalTime { get; set; }
    
    [DataType(DataType.Time)]
    public DateTime DepartureTime { get; set; }
    
    public int StopSequence { get; set; }
    public int PickupType { get; set; }
    public int DropoffType { get; set; }
    
    [ForeignKey("Stop")]
    public int StopId { get; set; }
    public virtual Stop Stop { get; set; }
    
    [ForeignKey("Trip")]
    public int TripId { get; set; }
    public virtual Trip Trip { get; set; }
}