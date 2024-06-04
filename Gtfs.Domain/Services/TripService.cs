using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gtfs.Domain.Services;

public class TripService
{
    private readonly IRepository<Trip, int> _tripRepo;

    public TripService(IRepository<Trip, int> tripRepo)
    {
        _tripRepo = tripRepo;
    }

    public async Task<List<Trip>> GetTripsByRoute(string agencyName, string routeNumber)
    {
        var trips = await _tripRepo.GetAll()
            .Where(trip => trip.Route.Agency != null 
                           && trip.Route.RouteNumber.Equals(routeNumber) 
                           && trip.Route.Agency.Name.Equals(agencyName))
            .ToListAsync();

        return trips; 
    }
    
    public async Task<List<int>> GetTripIdsByAgency(string agencyName)
    {
        var trips = await _tripRepo.GetAll()
            .Where(trip => trip.Route.Agency != null && trip.Route.Agency.Name.Equals(agencyName))
            .Select(trip => trip.Id)
            .ToListAsync();

        return trips;
    }
    
    public async Task<List<int>> GetTripIdsByRoute(string agencyName, string routeNumber)
    {
        var trips = await _tripRepo.GetAll()
            .Where(trip => trip.Route.Agency != null 
                           && trip.Route.RouteNumber.Equals(routeNumber) 
                           && trip.Route.Agency.Name.Equals(agencyName.ToUpper()))
            .Select(trip => trip.Id)
            .ToListAsync();

        return trips;
    }

    
    public async Task<List<int>> GetTripIdsFromRouteList(List<Route> routes, string agencyName)
    {

        var tripIds = new List<int>();

        foreach (var route in routes)
        {
            var trips = await GetTripsByRoute(agencyName, route.RouteNumber);

            tripIds.AddRange(trips.Select(trip => trip.Id));
        }        
        return tripIds;
    }
}