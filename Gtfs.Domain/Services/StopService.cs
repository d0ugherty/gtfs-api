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

    public async Task<List<Stop>> GetStopsByDataSource(string sourceName)
    {
        List<Stop> stops = await _stopRepo.GetAll()
            .Where(stop => stop.Source.Name.Equals(sourceName))
            .ToListAsync();

        return stops;
    }
}