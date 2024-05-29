using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gtfs.Domain.Services;

public class RouteService
{
    private readonly IRepository<Route, int> _routeRepo;

    public RouteService(IRepository<Route, int> routeRepo)
    {
        _routeRepo = routeRepo;
    }

    public async Task<Route> GetRouteById(int id)
    {
        var route = _routeRepo.GetById(id);

        return route;
    }

    public async Task<List<Route>> GetRoutesByAgencyName(string agencyName)
    {
        var routes = await _routeRepo.GetAll()
            .Where(r => r.Agency.Name.Equals(agencyName))
            .Select(r => new Route
            {
                RouteId = r.RouteId,
                ShortName = r.ShortName,
                LongName = r.LongName,
                Description = r.Description,
                Type = r.Type,
                Color = r.Color,
                TextColor = r.TextColor,
                Url = r.Url,
                Agency = r.Agency
            })
            .ToListAsync();
        
        return routes;
    }

    public async Task<List<Route>> GetRoutesBySource(string sourceName)
    {
        var routes = await _routeRepo.GetAll()
            .Include(r => r.Agency)
            .Where(r => r.Agency.Source.Name.Equals(sourceName))
            .Select(r => new Route
            {
                RouteId = r.RouteId,
                ShortName = r.ShortName,
                LongName = r.LongName,
                Description = r.Description,
                Type = r.Type,
                Color = r.Color,
                TextColor = r.TextColor,
                Url = r.Url,
                Agency = r.Agency
            })
            .ToListAsync();
        
        return routes;
    }

    public async Task<List<Route>> GetRoutesByAgencyAndType(string agencyName, int routeType)
    {
        var routes = await _routeRepo.GetAll()
            .Include(r => r.Trips)
            .Where(r => r.Agency.Name.Equals(agencyName) && r.Type == routeType)
            .Select(r => new Route
            {
                RouteId = r.RouteId,
                ShortName = r.ShortName,
                LongName = r.LongName,
                Description = r.Description,
                Type = r.Type,
                Color = r.Color,
                TextColor = r.TextColor,
                Url = r.Url,
                Agency = r.Agency
            })
            .ToListAsync();
        
        return routes;
    }
    
    public async Task<List<Route>> GetRoutesBySourceAndType(string sourceName, int routeType)
    {
        var routes = await _routeRepo.GetAll()
            .Where(r => r.Agency.Source.Name.Equals(sourceName) && r.Type == routeType)
            .Select(r => new Route
            {
                RouteId = r.RouteId,
                ShortName = r.ShortName,
                LongName = r.LongName,
                Description = r.Description,
                Type = r.Type,
                Color = r.Color,
                TextColor = r.TextColor,
                Url = r.Url,
                Agency = r.Agency
            })
            .ToListAsync();
        
        return routes;
    }
    
    public void AddRoute(Agency agency, string routeId, string routeShortName, string routeLongName,
        int type, string color, string textColor, string url)
    {
        var route = new Route
        {
            RouteId = routeId,
            ShortName = routeShortName,
            LongName = routeLongName,
            Type = type,
            Color = color,
            TextColor = textColor,
            Url = url,
            AgencyId = agency.Id,
            Agency = agency
        };

        _routeRepo.Add(route);
        agency.Routes.Add(route);
    }
}