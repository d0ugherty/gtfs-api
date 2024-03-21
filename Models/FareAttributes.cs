using System.ComponentModel.DataAnnotations.Schema;

namespace GtfsApi.Models;

public class FareAttributes
{
    public int Id { get; set; }
    
    public float Price { get; set; }
    public string? CurrencyType { get; set; }
    public int? PaymentMethod { get; set; }
    
    public int? Transfers { get; set; }
    public string? TransferDuration { get; set; }
    
    public string GtfsFareId { get; set; } = null!;

    [ForeignKey("Fare")] 
    public int FkFareId { get; set; }
    public virtual Fare Fare { get; set; } = null!;
}