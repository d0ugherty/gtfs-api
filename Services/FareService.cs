using GtfsApi.Interfaces;
using GtfsApi.Models;

namespace GtfsApi.Services;

public class FareService : IFareService
{
	private readonly GtfsContext _context;


	public async Task<Fare> GetFare(int id)
	{
		throw new NotImplementedException();
	}

	public async Task<Fare> GetFare(string origin, string destination)
	{
		throw new NotImplementedException();
	}

	public async Task<float> GetFarePrice(Fare fare)
	{
		throw new NotImplementedException();
	}
}