using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Models;

namespace Gtfs.Domain.Services;

public class StopService
{
    private readonly IRepository<Stop, int> _stopRepo;

    public StopService(IRepository<Stop, int> stopRepo)
    {
        _stopRepo = stopRepo;
    }

    public Task<List<Stop>> GetStopsByAgency()
    {
        throw new NotImplementedException();
    }
}