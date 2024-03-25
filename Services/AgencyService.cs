using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Services;

public class AgencyService(GtfsContext context) : IAgencyService

{
    /**
     * Retrieves all Agency rows associated with the data source
     * i.e. NJ Transit, SEPTA, or Amtrak
     */
    public async Task<List<Agency>> GetAllAgencies(string parentAgency)
    {
        var agencies = await context.Agencies
            .Where(agency => agency.ParentAgency.Name.Equals(parentAgency))
            .ToListAsync();

        return agencies;
    }
    
    public async Task<Agency> GetAgencyByGtfsId(string gtfsAgencyId)
    {
        var agency = await context.Agencies
            .Where(ag => ag.AgencyId.Equals(gtfsAgencyId))
            .FirstOrDefaultAsync();
        
        return agency;
    }
}