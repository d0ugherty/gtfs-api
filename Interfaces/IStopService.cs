using GtfsApi.Models;

namespace GtfsApi.Interfaces;

public interface IStopService
{
	public Task<List<Stop>> GetStopsById(List<int> stopIds);
}