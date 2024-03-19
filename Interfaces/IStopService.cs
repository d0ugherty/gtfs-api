using GtfsApi.Models;

namespace GtfsApi.Interfaces;

public interface IStopService
{
	public Task<List<Stop>> GetStopListAsync(List<int> stopIds);
	public Task<Stop> GetStopAsync(int id);

	public Task<List<StopTime>> GetStopTimesAsync(Stop stop);
}