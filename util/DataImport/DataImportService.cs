using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using GtfsApi.Models;

namespace DataImportUtility
{
    public class DataImportService
    {
        private readonly GtfsContext context;

        public DataImportService(GtfsContext context)
        {
            this.context = context;
        }

        public void ImportData(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<RoutesCsv>(); // Adjust according to your CSV structure or use a strongly typed class

                foreach (var record in records)
                {
                    // Assuming 'AgencyId' is the column name for the agency name in your CSV
                    var agencyId = record.agency_id;
                    var agency = context.Agencies.SingleOrDefault(a => a.AgencyId == agencyId)
                                 ?? context.Agencies.Add(new Agency { AgencyId = agencyId }).Entity;

                    context.GtfsRoutes.Add(new GtfsRoute
                    {
                        RouteId = record.route_id,
                        ShortName = record.route_short_name,
                        LongName = record.route_long_name,
                        Color = record.route_color, 
                        TextColor = record.route_text_color,
                        Url = record.route_url,
                        AgencyId = record.agency_id,
                        FkAgencyId = agency.Id
                    });
                }

                context.SaveChanges();
            }
        }
    }
}