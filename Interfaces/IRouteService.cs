using GtfsApi.Models;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Interfaces;

public interface IRouteService
{
    Task<Route> GetRouteAsync(string agencyId, string routeId);
    Task<List<Route>> GetAllRoutesAsync(string agencyId);
    Task<List<Trip>> GetRouteTripsAsync(int routeId, int results);
}