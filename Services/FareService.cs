using GtfsApi.Interfaces;
using GtfsApi.Migrations;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Services;

public class FareService : IFareService
{
	private readonly GtfsContext _context;

	public FareService(GtfsContext context)
	{
		_context = context;
	}

	public async Task<Fare> GetFare(int id)
	{
		Fare fare = await _context.Fares
			.Where(fare => fare.Id == id)
			.SingleOrDefaultAsync() ?? throw new InvalidOperationException();

		return fare;
	}

	public async Task<Fare> GetFare(string origin, string destination)
	{
		
		string originZone = await _context.Stops
			.Where(stop => stop.Name.Equals(origin))
			.Select(stop => stop.ZoneId)
			.FirstOrDefaultAsync() ?? string.Empty;

		string destinationZone = await _context.Stops
			.Where(stop => stop.Name.Equals(destination))
			.Select(stop => stop.ZoneId)
			.FirstOrDefaultAsync() ?? string.Empty;
		
		Fare fare = await _context.Fares
			.FirstOrDefaultAsync(fare => fare.OriginId.Equals(originZone)
			                             && fare.DestinationId.Equals(destinationZone)) 
		            ?? throw new InvalidOperationException();

		return fare;
	}

	public async Task<float> GetFarePrice(Fare fare)
	{
		float price = await _context.FareAttributesTbl
			.Where(fa => fa.Fk_fareId == fare.Id)
			.Select(fa => fa.Price).SingleAsync();

		return price;
	}

	public async Task<FareAttributes> GetFareAttributes(Fare fare)
	{
		FareAttributes attributes = await _context.FareAttributesTbl
			.Where(fa => fa.Fk_fareId == fare.Id)
			.Select(fa => fa)
			.SingleAsync();

		return attributes;
	}
}