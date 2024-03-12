using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper.Configuration;
using GtfsApi.Models;
using Calendar = GtfsApi.Models.Calendar;

namespace DataImportUtility
{
    public class DataImportService
    {
        private readonly GtfsContext _context;

        public DataImportService(GtfsContext context)
        {
            this._context = context;
        }

        private void ImportAgency(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<AgencyCsv>();

                foreach (var record in records)
                {
                    _context.Agencies.Add(new Agency
                    {
                        AgencyId = record.agency_id,
                        Name = record.agency_name,
                        Url = record.agency_url,
                        Timezone = record.agency_timezone,
                        Language = record.agency_lang,
                        Email = record.agency_email
                    });
                }
            }
        } 

        private void ImportRoutes(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<RoutesCsv>();

                foreach (var record in records)
                {
                    var agencyId = record.agency_id;
                    var agency = _context.Agencies.SingleOrDefault(a => a.AgencyId == agencyId)
                                 ?? _context.Agencies.Add(new Agency { AgencyId = agencyId }).Entity;

                    _context.GtfsRoutes.Add(new GtfsRoute
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

                _context.SaveChanges();
            }
        }

        private void ImportCalendar(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<CalendarCsv>();

                foreach (var record in records)
                {
                    _context.Calendars.Add(new Calendar
                    {
                        ServiceId = record.service_id,
                        Monday = record.monday,
                        Tuesday = record.tuesday,
                        Wednesday = record.wednesday,
                        Thursday = record.thursday,
                        Friday = record.friday,
                        Saturday = record.saturday,
                        Sunday = record.sunday,
                        StartDate = record.start_date,
                        EndDate = record.end_date

                    });
                }

                _context.SaveChanges();
            }
        }

        private void ImportCalendarDates(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<CalendarDatesCsv>();

                foreach (var record in records)
                {
                    _context.CalendarDates.Add(new CalendarDate
                    {  
                        ServiceId = record.service_id,
                        Date = record.date,
                        ExceptionType = record.exception_type
                    });
                }

                _context.SaveChanges();
            }
        }

        public void ImportData()
        {
            ImportAgency("../../data/agency.csv");
            Console.WriteLine("Agencies successfully imported.");
            
            ImportRoutes("../../data/routes.csv");
            Console.WriteLine("Routes successfully imported.");
            
            ImportCalendar("../../data/calendar.csv");
            Console.WriteLine("Calendar successfully imported.");
            
            ImportCalendarDates("../../data/calendar_dates.csv");
            Console.WriteLine("Calendar dates successfully imported.");
            
            
        }
    }
}