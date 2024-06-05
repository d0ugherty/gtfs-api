using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gtfs.Domain.Services;

public class StopTimeService
{
	private readonly IRepository<StopTime, int> _stopTimeRepo;

	public StopTimeService(IRepository<StopTime, int> stopTimeRepo)
	{
		_stopTimeRepo = stopTimeRepo;
	}

	public async Task<List<StopTime>> GetStopTimesFromTripList(List<Trip> trips)
	{
		List<StopTime> stopTimes = new List<StopTime>();

		foreach (var trip in trips)
		{
			var tripStopTimes = await _stopTimeRepo.GetAll()
				.Where(st => st.TripId == trip.Id)
				.ToListAsync();

			stopTimes.AddRange(tripStopTimes);
		}
		
		return stopTimes;
	}
}