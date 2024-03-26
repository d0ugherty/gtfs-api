using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.Data.SqlClient;
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
            .FromSqlRaw("SELECT DISTINCT s.* " +
                        "FROM Stops as s " +
                        "JOIN StopTimes st ON s.Id = st.Fk_stopId " +
                        "JOIN Trips t ON st.Fk_tripId = t.Id " +
                        "JOIN Routes r ON t.Fk_routeId = r.Id " +
                        "WHERE r.RouteId = {0} AND r.GtfsAgencyId = {1} ", routeId, agencyId 
                        ).ToListAsync();

        return stops;
    }

    public async Task<List<Route>> GetRoutesByTypeAsync(string agencyName, int routeType)
    {
        var agency = _context.Agencies
            .FirstOrDefault(agency => agency.Name.Equals(agencyName));
        
        var parentAgency = _context.ParentAgencies
            .FirstOrDefault(pa => agency != null && pa.Id == agency.Fk_parentAgencyId);
        
        List<Route> routes = await _context.Routes
            .Where(route => parentAgency != null && route.Agency.ParentAgency.Id == parentAgency.Id && route.Type == routeType)
            .ToListAsync();

        return routes;
    }

    public async Task<List<Shape>> GetRouteShapesAsync(string agencyId, string routeId, int pageNumber)
    {
        Trip trip = await _context.Trips.FirstAsync(tr => tr.GtfsRouteId.Equals(routeId));
        
        const int pageSize = 100;
        int skip = (pageNumber - 1) * pageSize;

        var shapeDataPage = await _context.Shapes
            .Where(shape => shape.ShapeId == trip.ShapeId)
            .OrderBy(shape => shape.Id) 
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
        return shapeDataPage;
    }
}