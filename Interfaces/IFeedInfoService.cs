using GtfsApi.Models;

namespace GtfsApi.Interfaces;

public interface IFeedInfoService
{
	public Task<List<FeedInfo>> GetFeedInfo(string agencyId);
}