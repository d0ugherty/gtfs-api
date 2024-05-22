using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using DataImport.Models;
using Gtfs.DataAccess;
using Gtfs.DataAccess.Repository;
using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Models;
using Calendar = Gtfs.Domain.Models.Calendar;

namespace DataImport;

public class DataImport
{
    private readonly GtfsContext _context;

    private readonly IRepository<Source, int> _sourceRepo;
    private readonly IRepository<Agency, string> _agencyRepo;
    private readonly IRepository<Route, int> _routeRepo;
    private readonly IRepository<Calendar, int> _calendarRepo;
    private readonly IRepository<CalendarDate, int> _calendarDateRepo;
    private readonly IRepository<Fare, string> _fareRepo;
    private readonly IRepository<FareAttributes, int> _fareAttributesRepo;
    private readonly IRepository<Shape, int> _shapeRepo;
    private readonly IRepository<Stop, int> _stopRepo;
    private readonly IRepository<StopTime, int> _stopTimeRepo;
    private readonly IRepository<Trip, int> _tripRepo;


    public DataImport(GtfsContext context)
    {
        _context = context;

        _sourceRepo = new Repository<Source, int>(_context);
        
        _agencyRepo = new Repository<Agency, string>(_context);
        
        _routeRepo = new Repository<Route, int>(_context);
        
        _calendarRepo = new Repository<Calendar, int>(_context);
        
        _calendarDateRepo = new Repository<CalendarDate, int>(_context);
        
        _fareRepo = new Repository<Fare, string>(_context);
        
        _fareAttributesRepo = new Repository<FareAttributes, int>(_context);
        
        _shapeRepo = new Repository<Shape, int>(_context);
        
        _stopRepo = new Repository<Stop, int>(_context);
        
        _stopTimeRepo = new Repository<StopTime, int>(_context);
        
        _tripRepo = new Repository<Trip, int>(_context);
    }

    private void ImportSources(string filePath)
    {
        var records = ReadCsv<SourceCsv>(filePath);

        try
        {
            int row = 1;
            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                _sourceRepo.Add(new Source
                {
                    Name = record.name.Trim(),
                    FilePath = "../data/" + record.name
                });
                row++;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportAgencies(string filePath, Source source)
    {
        var records = ReadCsv<AgencyCsv>(filePath);
        try
        {
            int row = 1;
            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var agency = new Agency
                {
                    AgencyId = record.agency_id.Trim(),
                    Name = record.agency_name.Trim(),
                    Url = record.agency_url!.Trim(),
                    Timezone = record.agency_timezone!.Trim(),
                    Language = record.agency_lang!.Trim(),
                    Email = record.agency_email!.Trim(),
                    SourceId = source.Id,
                    Source = source
                };

                _agencyRepo.Add(agency);

                source.Agencies.Add(agency);

                row++;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportRoutes(string filePath, Source source)
    {
        var records = ReadCsv<RoutesCsv>(filePath);
        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var agency = _agencyRepo.GetAll()
                    .FirstOrDefault(a => a.AgencyId.Equals(record.agency_id) && a.SourceId == source.Id);

                _routeRepo.Add(new Route
                {
                    RouteId = record.route_id,
                    ShortName = record.route_short_name,
                    LongName = record.route_long_name,
                    Description = record.route_desc,
                    Type = record.route_type,
                    Color = record.route_color,
                    TextColor = record.route_text_color,
                    Url = record.route_url,
                    AgencyId = agency!.Id,
                    Agency = agency
                });
                row++;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportCalendars(string filePath, Source source)
    {
        var records = ReadCsv<CalendarCsv>(filePath);

        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                var calendar = new Calendar
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
                    EndDate = record.end_date,
                    SourceId = source.Id,
                    Source = source
                };

                _calendarRepo.Add(calendar);

                source.Calendars.Add(calendar);

                row++;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportCalendarDates(string filePath)
    {
        var records = ReadCsv<CalendarDatesCsv>(filePath);
        
        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                _calendarDateRepo.Add(new CalendarDate
                {
                    ServiceId = record.service_id,
                    Date = record.date,
                    ExceptionType = record.exception_type
                });
                row++;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportFares(string filePath, Source source)
    {
        var records = ReadCsv<FaresCsv>(filePath);

        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                _fareRepo.Add(new Fare
                {
                    FareId = record.fare_id,
                    OriginId = record.origin_id,
                    DestinationId = record.destination_id,
                    SourceId = source.Id,
                    Source = source
                });

                row++;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
    }

    private void ImportFareAttributes(string filePath)
    {
        var records = ReadCsv<FareAttributesCsv>(filePath);

        try
        {
            int row = 1;

            foreach (var record in records)
            {
                Console.Write($"{new string(' ', 20)}Importing row {row}\r");

                string fareId = record.fare_id;

                var fare = _fareRepo.GetById(fareId);

                var fareAttributes = new FareAttributes
                {
                    Price = record.price,
                    CurrencyType = record.currency_type,
                    PaymentMethod = record.payment_method,
                    Transfers = record.transfers,
                    TransferDuration = record.transfer_duration,
                    FareId = fare.Id,
                    Fare = fare
                };

                _fareAttributesRepo.Add(fareAttributes);

                fare.FareAttributesId = fareAttributes.Id;
                fare.FareAttributes = fareAttributes;
                _context.Update(fare);

                row++;
            }
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Invalid operation occurred during importing of \n " +
                              $"{filePath} \n " +
                              $"{ex}");
            throw;
        }
        
    }

    private void ImportShapes(string filePath)
    {
        throw new NotImplementedException();
    }

    private void ImportStops(string filePath)
    {
        throw new NotImplementedException();
    }

    private void ImportStopTimes(string filePath)
    {
        throw new NotImplementedException();
    }

    private void ImportTrips(string filePath)
    {
        throw new NotImplementedException();
    }

    private IEnumerable<T> ReadCsv<T>(string filePath)
    {
        using (var reader = new StreamReader(filePath))
        {
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();

                return csv.GetRecords<T>();
            }
        }
    }


    private void ImportTry(string filePath, Action<string> import)
    {
        if (File.Exists(filePath))
        {
            try
            {
                Console.WriteLine($"Importing {filePath}");
                import(filePath);
                Console.WriteLine($"{filePath} \n successfully imported.");
            }
            catch (ReaderException ex)
            {
                Console.WriteLine($"{ex} \n Reader exception occurred, moving on to next import");
            }
        }
        else
        {
            Console.WriteLine($"{filePath} does not exist. \n Moving on to next import.");
        }
    }
    
    public void ImportData()
    {
        ImportTry($"../data/sources.csv", ImportSources);

        var sources = _sourceRepo.GetAll();

        foreach (var source in sources)
        {
            ImportTry($"{source.FilePath}/agency.csv", filePath => ImportAgencies(filePath, source));
            
            ImportTry($"{source.FilePath}/calendar.csv", filePath => ImportCalendars(filePath, source));
            
            ImportTry($"{source.FilePath}/calendar_dates.csv", ImportCalendarDates);
            
            ImportTry($"{source.FilePath}/fare_rules.csv", filePath => ImportFares(filePath, source));
            
            ImportTry($"{source.FilePath}/fare_attributes.csv", ImportFareAttributes);

            ImportTry($"{source.FilePath}/routes.csv", filePath => ImportRoutes(filePath, source));

            ImportTry($"{source.FilePath}/stops.csv", ImportStops);

            ImportTry($"{source.FilePath}/stop_times.csv", ImportStopTimes);

            ImportTry($"{source.FilePath}/shapes.csv", ImportShapes);

            ImportTry($"{source.FilePath}/trips.csv", ImportTrips);
        }

        Console.WriteLine("Done.");
    }
}