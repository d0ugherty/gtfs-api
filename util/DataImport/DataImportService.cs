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

                _context.SaveChanges();
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
                        AgencyId = agency.Id
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

        private void ImportStops(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<StopsCsv>();

                foreach (var record in records)
                {
                    _context.Stops.Add(new Stop
                    {  
                        StopId = record.stop_id,
                        Name = record.stop_name,
                        Description = record.stop_desc,
                        Latitude = record.stop_lat,
                        Longitude = record.stop_lon,
                        ZoneId = record.zone_id,
                        Url = record.stop_url
                    });
                }

                _context.SaveChanges();
            }
        }

        private void ImportShapes(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<ShapesCsv>();

                foreach (var record in records)
                {
                    _context.Shapes.Add(new Shape
                    {
                        ShapeId = record.shape_id,
                        ShapePtLat = record.shape_pt_lat,
                        ShapePtLon = record.shape_pt_lon,
                        Sequence = record.shape_pt_sequence,
                        DistanceTraveled = record.shape_dist_traveled
                    });
                }
                _context.SaveChanges();
            }
        }
        
        private void ImportTrips(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<TripsCsv>();

                foreach (var record in records)
                {
                    var tripShapeId = record.shape_id;
                    var shape = _context.Shapes.SingleOrDefault(sh => sh.ShapeId == tripShapeId)
                                ?? _context.Shapes.Add(new Shape { ShapeId = tripShapeId }).Entity;

                    var tripRouteId = record.route_id;
                    var route = _context.GtfsRoutes.SingleOrDefault(rt => rt.RouteId == tripRouteId)
                                ?? _context.GtfsRoutes.Add(new GtfsRoute { RouteId = tripRouteId }).Entity;

                    _context.Trips.Add(new Trip
                    {
                        ServiceId = record.service_id,
                        TripId = record.trip_id,
                        Headsign = record.trip_headsign,
                        BlockId = record.block_id,
                        ShortName = record.trip_short_name,
                        DirectionId = record.direction_id,
                        RouteId = route.Id,
                        ShapeId = shape.Id
                    });
                }
                _context.SaveChanges();
            }
        }

        private void ImportStopTimes(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<StopTimesCsv>();

                foreach (var record in records)
                {
                    var stop = _context.Stops.SingleOrDefault(st => st.StopId == record.stop_id)
                                ?? _context.Stops.Add(new Stop { StopId = record.stop_id }).Entity;

                    var trip = _context.Trips.SingleOrDefault(tr => tr.TripId == record.trip_id)
                               ?? _context.Trips.Add(new Trip { TripId = record.trip_id }).Entity;
                    
                    _context.StopTimes.Add(new StopTime
                    {
                       ArrivalTime = record.arrival_time,
                       DepartureTime = record.departure_time,
                       StopSequence = record.stop_sequence,
                       PickupType = record.pickup_type,
                       DropoffType = record.dropoff_type,
                       StopId = stop.Id,
                       TripId = trip.Id
                    });
                }

                _context.SaveChanges();
            }
        }

        private void ImportFares(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<FaresCsv>();

                foreach (var record in records)
                {
                    _context.Fares.Add(new Fare
                    {
                       FareId = record.fare_id,
                       OriginId = record.origin_id,
                       DestinationId = record.destination_id
                    });
                }
                _context.SaveChanges();
            }
        }

        private void ImportFareAttributes(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                var records = csv.GetRecords<FareAttributesCsv>();

                foreach (var record in records)
                {
                    var fareId = record.fare_id;
                    var fare = _context.Fares.SingleOrDefault(f => f.FareId == fareId)
                               ?? _context.Fares.Add(new Fare { FareId = fareId }).Entity;
                    
                    _context.FareAttributesEnumerable.Add(new FareAttributes
                    {
                        Price = record.price,
                        CurrencyType = record.currency_type,
                        PaymentMethod = record.payment_method,
                        Transfers = record.transfers,
                        TransferDuration = record.transfer_duration,
                        FareId = fare.Id
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
            
            ImportStops("../../data/stops.csv");
            Console.WriteLine("Stops successfully imported.");
            
            ImportShapes("../../data/shapes.csv");
            Console.WriteLine("Shapes successfully imported.");
            
            ImportTrips("../../data/trips.csv");
            Console.WriteLine("Trips successfully imported.");
            
            ImportStopTimes("../../data/stop_times.csv");
            Console.WriteLine("Stop times successfully imported.");
            
            ImportFares("../../data/fare_rules.csv");
            Console.WriteLine("Fare rules successfully imported.");
            
            ImportFareAttributes("../../data/fare_attributes.csv");
            Console.WriteLine("Fare attributes successfully imported");
            
        }
    }
}