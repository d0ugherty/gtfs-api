using GtfsApi.Models;

namespace GtfsApi.Interfaces;

public interface IFareService
{
	public Task<Fare> GetFare(int id);

	public Task<Fare> GetFare(string origin, string destination);

	public Task<float> GetFarePrice(Fare fare);
	
	public Task<FareAttributes> GetFareAttributes(Fare fare);
}