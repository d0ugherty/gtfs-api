using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Services;

public class StopService : IStopService
{
	private readonly GtfsContext _context;

	public StopService(GtfsContext context)
	{
		_context = context;
	}
	
	public async Task<List<Stop>> GetStopsById(List<int> stopIds)
	{
		List<Stop> stops;
		
		stops = await _context.Stops
			.Where(stop => stopIds.Contains(stop.Id))
			.Select(stop => stop)
			.ToListAsync();

		return stops;
	}
}