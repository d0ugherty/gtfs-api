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
	
	public async Task<List<Stop>> GetStopListAsync(List<int> stopIds)
	{
		List<Stop> stops = await _context.Stops
			.Where(stop => stopIds.Contains(stop.Id))
			.Select(stop => stop)
			.ToListAsync();

		return stops;
	}

	public async Task<Stop> GetStopAsync(int id)
	{
		Stop stop = await _context.Stops
			.Where(stop => stop.Id == id)
			.SingleOrDefaultAsync() ?? throw new InvalidOperationException();

		return stop;
	}

	public async Task<List<StopTime>> GetStopTimesAsync(Stop stop)
	{
		List<StopTime> stopTimes = await _context.StopTimes
			.Where(st => st.Stop.Id == stop.Id)
			.Select(st => st)
			.ToListAsync();

		return stopTimes;
	}
}