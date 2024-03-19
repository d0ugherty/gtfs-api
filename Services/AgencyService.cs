using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Services;

public class AgencyService : IAgencyService

{
    private readonly GtfsContext _context;

    public AgencyService(GtfsContext context)
    {
        _context = context;
    }

    public async Task<List<Agency>> GetAllAgencies()
    {
        var agencies = await _context.Agencies.ToListAsync();

        return agencies;
    }
    
    public async Task<Agency?> GetAgencyByGtfsId(string gtfsAgencyId)
    {
        var agency = await _context.Agencies
            .Where(ag => ag.AgencyId.Equals(gtfsAgencyId))
            .FirstOrDefaultAsync();
        
        return agency;
    }
}