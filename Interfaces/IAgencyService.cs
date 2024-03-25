namespace GtfsApi.Interfaces;
using GtfsApi.Models;

public interface IAgencyService
{
    public Task<List<Agency>> GetAllAgencies(string parentAgencyName);
    public Task<Agency> GetAgencyByGtfsId(string gtfsAgencyId);
}