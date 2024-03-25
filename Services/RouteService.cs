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
            .Where(rt => rt.GtfsAgencyId.Equals(agencyId))
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
            .Where(trip => routeId == trip.Fk_routeId)
            .ToListAsync();

        return trips;
    }

    public async Task<List<Trip>> GetRouteTripsAsync(List<int> routeIds)
    {
        List<Trip> trips = await _context.Trips
            .Where(trip => routeIds.Contains(trip.Fk_routeId))
            .ToListAsync();

        return trips;
    }

    public async Task<List<int>> GetRouteStopIds(List<Trip> trips)
    {
        List<int> tripIds = trips.Select(trip => trip.Id).ToList();

        List<StopTime?> stopTimes = await _context.StopTimes
            .Where(stopTime => tripIds.Contains(stopTime.Fk_tripId))
            .GroupBy(stopTime => stopTime.Fk_stopId)
            .Select(stopId => stopId.FirstOrDefault())
            .ToListAsync();
         
       List<int> stopIds = stopTimes.Select(stopTime => stopTime!.Fk_stopId).ToList();
        
       return stopIds;
    }

    public async Task<List<Stop>> GetRouteStops(string agencyId, string routeId)
    {
        List<Stop> stops = await _context.Stops
            .FromSqlRaw("SELECT s.* " +
                        "FROM Stops as s " +
                        "JOIN StopTimes st ON s.Id = st.Fk_stopId " +
                        "JOIN Trips t ON st.Fk_tripId = t.Id " +
                        "JOIN Routes r ON t.Fk_tripId = t.Id " +
                        "WHERE r.RouteId = {0} AND r.AgencyId {1} ", routeId, agencyId 
                        ).ToListAsync();

        return stops;
    }

    public async Task<List<Route>> GetRoutesByTypeAsync(string agencyId, int routeType)
    {
        List<Route> routes = await _context.Routes
            .Where(rt => rt.GtfsAgencyId.Equals(agencyId) && rt.Type == routeType)
            .ToListAsync();

        return routes;
    }
}