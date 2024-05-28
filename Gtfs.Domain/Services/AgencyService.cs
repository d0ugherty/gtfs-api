using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Gtfs.Domain.Services;

public class AgencyService
{
    private readonly IRepository<Agency, int> _agencyRepo;

    public AgencyService(IRepository<Agency, int> agencyRepo)
    {
        _agencyRepo = agencyRepo;
    }

    public async Task<Agency> GetAgencyByName(string agencyName)
    {
        var agency = await _agencyRepo.GetAll()
            .FirstOrDefaultAsync(a => a.Name.Equals(agencyName));

        return agency;
    }

    public async Task<Agency> GetAgencyById(int id)
    {
        var agency = _agencyRepo.GetById(id);

        return agency;
    }

    public async Task<List<Agency>> GetAgenciesBySource(string sourceName)
    {
        var agencies = await _agencyRepo.GetAll()
            .Where(a => a.Source.Name.Equals(sourceName))
            .ToListAsync();

        return agencies;
    }
}