using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Services;

public class RouteService : IRouteService
{
    private readonly GtfsContext _context;

    public RouteService(GtfsContext context)
    {
        _context = context;
    }

    public async Task<List<Route>> GetAgencyRoutesAsync(string agencyId)
    {
        List<Route> routes = await _context.Routes
            .Where(rt => rt.GtfsAgencyId.Equals(agencyId.ToUpper()))
            .ToListAsync();
        
        return routes;
    }

    public async Task<Route> GetRouteAsync(string agencyId, string gtfsRouteId)
    {
        var route = await _context.Routes
            .Where(rt => rt.GtfsAgencyId.Equals(agencyId.ToUpper())
                         && rt.RouteId.Equals(gtfsRouteId.ToUpper()))
            .FirstOrDefaultAsync() ?? throw new InvalidOperationException();
            
        return route;
    }

    public async Task<List<Trip>> GetRouteTripsAsync(int routeId)
    {
        List<Trip> trips = await _context.Trips
            .Where(trip => routeId == trip.FkRouteId)
            .ToListAsync();

        return trips;
    }

    public async Task<List<Trip>> GetRouteTripsAsync(List<int> routeIds)
    {
        List<Trip> trips;

        trips = await _context.Trips
            .Where(trip => routeIds.Contains(trip.FkRouteId))
            .ToListAsync();

        return trips;
    }

    public async Task<List<int>> GetRouteStopIds(List<Trip> trips)
    {
        List<int> tripIds = trips.Select(trip => trip.Id).ToList();

        List<StopTime?> stopTimes = await _context.StopTimes
            .Where(stopTime => tripIds.Contains(stopTime.FkTripId))
            .GroupBy(stopTime => stopTime.FkStopId)
            .Select(stopId => stopId.FirstOrDefault())
            .ToListAsync();
         
       List<int> stopIds = stopTimes.Select(stopTime => stopTime!.FkStopId).ToList();
        
       return stopIds;
    }
}