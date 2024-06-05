using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gtfs.Domain.Services;

public class StopService
{
    private readonly IRepository<Stop, int> _stopRepo;

    public StopService(IRepository<Stop, int> stopRepo)
    {
        _stopRepo = stopRepo;
    }

    public async Task<List<Stop>> GetStopsByTripIds(List<int> tripIds)
    {
        List<Stop> stops = await _stopRepo.GetAll()
            .Where(stop => stop.StopTimes.Any(st => tripIds.Contains(st.TripId)))
            .Select(stop => new Stop
            {
                StopNumber = stop.StopNumber,
                Name = stop.Name,
                Longitude = stop.Longitude,
                Url = stop.Url,
                ZoneId = stop.ZoneId,
                Description = stop.Description,
                Source = stop.Source
            })
            .ToListAsync();

        return stops;
    }

    public async Task<List<Stop>> GetStopsFromStopTimes(List<StopTime> stopTimes)
    {
        List<Stop> stops = new List<Stop>();

        foreach (var stopTime in stopTimes)
        {
            var stop = await _stopRepo.GetAll()
                .Where(s => s.Id == stopTime.Stop.Id)
                .SingleAsync();

            stops.Add(stop);
        }

        return stops;
    }
    
}