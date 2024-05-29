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

    public async Task<List<Trip>> GetTripsByRoute(int agencyId, string routeId)
    {
        var trips = await _tripRepo.GetAll()
            .Where(trip => trip.Route.RouteId.Equals(routeId) && trip.Route.AgencyId == agencyId)
            .ToListAsync();

        return trips;
    }
    
    public async Task<List<int>> GetTripIdsByRoute(string agencyName, string routeId)
    {
        var trips = await _tripRepo.GetAll()
            .Where(trip => trip.Route.RouteId.Equals(routeId) && trip.Route.Agency.Name.Equals(agencyName.ToUpper()))
            .Select(trip => trip.Id)
            .ToListAsync();

        return trips;
    }

    public async Task<List<int>> GetTripIdsFromRoute(List<Route> routes)
    {
        var tripIds = new List<int>();

        foreach (var route in routes)
        {
            Console.WriteLine($"ROUTE TRIPS {route.Trips}");
            Console.WriteLine($"ROUTE TRIPS LENGTH {route.Trips.Count}");

            Console.WriteLine($"ROUTE: {route.ShortName}");
            foreach (var trip in route.Trips)
            {
                Console.WriteLine($"TRIP {trip}");
            }
        }
        return tripIds;
    }
}