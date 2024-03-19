namespace GtfsApi.Interfaces;
using GtfsApi.Models;

public interface IAgencyService
{
    public Task<List<Agency>> GetAllAgencies();
    public Task<Agency> GetAgencyByGtfsId(string gtfsAgencyId);
}