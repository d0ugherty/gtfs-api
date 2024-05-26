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

    public async Task<List<Route>> GetRoutesByAgency(string agencyName)
    {
        var query = _routeRepo.GetAll()
            .Include(r => r.Agency)
            .Where(r => r.Agency.Name.Equals(agencyName));

        var routes = await query.ToListAsync();
        Console.WriteLine($"Found {routes.Count} routes for agency '{agencyName}'");
        
        routes = await _routeRepo.GetAll()
            .Include(r => r.Agency)
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

    public async Task<List<Route>> GetRoutesByAgencyAndType(string agencyName, int routeType)
    {
        var routes = await _routeRepo.GetAll()
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
}