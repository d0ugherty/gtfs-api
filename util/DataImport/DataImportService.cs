using CsvHelper;
using System.Globalization;
using System.Security.Principal;
using CsvHelper.Configuration;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using Calendar = GtfsApi.Models.Calendar;
using Route = GtfsApi.Models.Route;

namespace DataImportUtility
{
    public class DataImportService
    {
        private readonly GtfsContext _context;
        private readonly List<string> agencies = ["septa", "njt"];
        private readonly List<string> modes = ["rail", "bus"];
        public DataImportService(GtfsContext context)
        {
            this._context = context;
        }

        private void ImportAgency(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                   MissingFieldFound = null
                }))
            {
                csv.Read();
                csv.ReadHeader();
                
                var records = csv.GetRecords<AgencyCsv>();
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            _context.Agencies.Add(new Agency
                            {
                                AgencyId = record.agency_id.ToUpper(),
                                Name = record.agency_name.ToUpper(),
                                Url = record.agency_url,
                                Timezone = record.agency_timezone,
                                Language = record.agency_lang,
                                Email = record.agency_email
                            });
                        }

                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                          $"{filePath} \n " +
                                          $"{ex}");
                        throw;
                    }
                }
            }
        } 

        private void ImportRoutes(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();
                var records = csv.GetRecords<RoutesCsv>();
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            var agencyId = record.agency_id.ToUpper();
                            var agency = _context.Agencies.SingleOrDefault(a => a.AgencyId.Equals(agencyId)) 
                                         ?? _context.Agencies.Add(new Agency { AgencyId = agencyId, Name = agencyId }).Entity ;

                            _context.Routes.Add(new Route
                            {
                                RouteId = record.route_id,
                                ShortName = record.route_short_name,
                                LongName = record.route_long_name,
                                Color = record.route_color,
                                TextColor = record.route_text_color,
                                Type = record.route_type,
                                Url = record.route_url,
                                GtfsAgencyId = record.agency_id,
                                Agency = agency,
                                FkAgencyId = agency.Id
                            });
                        }
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                          $"{filePath} \n " +
                                          $"{ex}");
                        throw;
                    }
                }
            }
        }

        private void ImportCalendar(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();
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
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();
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
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();
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
            Console.WriteLine("Importing shapes may take a while...");
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            }))
            {
                var records = csv.GetRecords<ShapesCsv>();
                csv.Read();
                csv.ReadHeader();

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
        
        private void ImportTrips(string filePath, string agencyId)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();
                var records = csv.GetRecords<TripsCsv>();
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var agency = _context.Agencies.FirstOrDefault(ag => ag.AgencyId.Equals(agencyId.ToUpper()));
                        foreach (var record in records)
                        {
                            var tripRouteId = record.route_id;
                            var route = _context.Routes.FirstOrDefault(rt =>
                                            rt.RouteId.Equals(tripRouteId) && rt.Agency.Name.Equals(agency!.Name))
                                        ?? _context.Routes.Add(new Route
                                        {
                                            RouteId = tripRouteId,
                                            Agency = agency,
                                            FkAgencyId = agency.Id,
                                            GtfsAgencyId = agency.AgencyId
                                        }).Entity;

                            _context.Trips.Add(new Trip
                            {
                                ServiceId = record.service_id,
                                TripId = record.trip_id,
                                Headsign = record.trip_headsign,
                                BlockId = record.block_id,
                                ShortName = record.trip_short_name,
                                DirectionId = record.direction_id,
                                GtfsRouteId = record.route_id,
                                Route = route!,
                                FkRouteId = route!.Id,
                                ShapeId = record.shape_id
                            });
                        }
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                          $"{filePath} \n " +
                                          $"{ex}");
                        throw;
                    }
                }
            }
        }

        private void ImportStopTimes(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();
                var records = csv.GetRecords<StopTimesCsv>();
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            var stop = _context.Stops.FirstOrDefault(st => st.StopId == record.stop_id);

                            var trip = _context.Trips.FirstOrDefault(tr => tr.TripId == record.trip_id);

                            _context.StopTimes.Add(new StopTime
                            {
                                ArrivalTime = record.arrival_time,
                                DepartureTime = record.departure_time,
                                StopSequence = record.stop_sequence,
                                PickupType = record.pickup_type,
                                DropoffType = record.drop_off_type,
                                GtfsStopId = record.stop_id,
                                GtfsTripId = record.trip_id,
                                Stop = stop!,
                                FkStopId = stop!.Id,
                                Trip = trip!,
                                FkTripId = trip!.Id,
                            });
                        }

                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                          $"{filePath} \n " +
                                          $"{ex}");
                        throw;
                    }
                }
            }
        }

        private void ImportFares(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();
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
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();
                var records = csv.GetRecords<FareAttributesCsv>();
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            var fareId = record.fare_id;
                            var fare = _context.Fares.SingleOrDefault(f => f.FareId == fareId)
                                       ?? _context.Fares.Add(new Fare { FareId = fareId }).Entity;

                            _context.FareAttributesTbl.Add(new FareAttributes
                            {
                                Price = record.price,
                                CurrencyType = record.currency_type,
                                PaymentMethod = record.payment_method,
                                Transfers = record.transfers,
                                TransferDuration = record.transfer_duration,
                                GtfsFareId = record.fare_id,
                                Fare = fare,
                                FkFareId = fare.Id
                            });
                        }

                        _context.SaveChanges();
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                          $"{filePath} \n " +
                                          $"{ex}");
                        throw;
                    }
                }
            }
        }

        private static void ImportTry(string filePath, Action<string> import)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    import(filePath);
                    Console.WriteLine($"{filePath} \n successfully imported.");
                }
                catch (ReaderException ex)
                {
                    Console.WriteLine("Reader exception occurred, moving on to next import.");
                }
            }
            else
            {
                Console.WriteLine($"{filePath} does not exist. \n Moving on to next import.");
            }
        }
        
        public void ImportData()
        {
            foreach (var agency in agencies)
            {
                foreach (var mode in modes)
                {
                    ImportTry($"../../data/{agency}_{mode}/agency.csv", ImportAgency);

                    ImportTry($"../../data/{agency}_{mode}/routes.csv", ImportRoutes);

                    ImportTry($"../../data/{agency}_{mode}/calendar.csv", ImportCalendar);

                    ImportTry($"../../data/{agency}_{mode}/calendar_dates.csv", ImportCalendarDates);

                    ImportTry($"../../data/{agency}_{mode}/stops.csv", ImportStops);

                    ImportTry($"../../data/{agency}_{mode}/shapes.csv", ImportShapes);

                    ImportTry($"../../data/{agency}_{mode}/trips.csv", filePath => ImportTrips(filePath, agency));

                    ImportTry($"../../data/{agency}_{mode}/stop_times.csv", ImportStopTimes);

                    ImportTry($"../../data/{agency}_{mode}/fare_rules.csv", ImportFares);

                    ImportTry($"../../data/{agency}_{mode}/fare_attributes.csv", ImportFareAttributes);
                }

            }
        }
    }

}