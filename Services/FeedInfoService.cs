using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GtfsApi.Services;

public class FeedInfoService(GtfsContext context) : IFeedInfoService
{
	public async Task<List<FeedInfo>> GetFeedInfo(string agencyId)
	{
		var agencyFeedInfo = await context.FeedInfoTbl
			.FromSqlRaw("SELECT * " +
			            "FROM FeedInfoTbl AS fi " +
			            "JOIN Agencies a ON a.Id = fi.FkAgencyId" +
			            "WHERE a.Name = {0} OR a.AgencyId = {0}", agencyId)
			.ToListAsync();

		return agencyFeedInfo;
	}
}