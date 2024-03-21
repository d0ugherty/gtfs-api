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
			.Select(stop => stop.ZoneId).FirstOrDefaultAsync() ?? throw new InvalidOperationException();

		originZone = originZone.Trim();
		
		Console.WriteLine($"origin:____{originZone}_--");
		
		string destinationZone = await _context.Stops
			.Where(stop => stop.Name.Equals(destination))
			.Select(stop => stop.ZoneId).FirstOrDefaultAsync() ?? throw new InvalidOperationException();

		Console.WriteLine($"dest: ___{destinationZone}");
		
		var  test = _context.Fares.FirstOrDefault(fare => fare.Id == 1);
		
		Console.WriteLine($"Test value ____{test.OriginId}____");
		Console.WriteLine($"TEST DEST:---{test.DestinationId}---");

		var fare = await _context.Fares
			.FirstOrDefaultAsync(fare => fare.OriginId.Equals(originZone) && fare.DestinationId.Equals(destinationZone));

		Console.WriteLine($"fare = {fare}");

		return fare;
	}

	public async Task<float> GetFarePrice(Fare fare)
	{
		float price = await _context.FareAttributesTbl
			.Where(fa => fa.FkFareId == fare.Id)
			.Select(fa => fa.Price).SingleAsync();

		return price;
	}

	public async Task<FareAttributes> GetFareAttributes(Fare fare)
	{
		FareAttributes attributes = await _context.FareAttributesTbl
			.Where(fa => fa.FkFareId == fare.Id)
			.Select(fa => fa).SingleAsync();

		return attributes;
	}
}