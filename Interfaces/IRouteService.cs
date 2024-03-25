using GtfsApi.Models;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Interfaces;

public interface IRouteService
{
    public Task<Route> GetRouteAsync(string agencyId, string routeId);

    public Task<List<Route>> GetAgencyRoutesAsync(string agencyId, int type);
    
    public Task<List<Trip>> GetRouteTripsAsync(int routeId);
    
    public Task<List<Trip>> GetRouteTripsAsync(List<int> routeId);
    
    public Task<List<int>> GetRouteStopIds(List<Trip> trips);
}