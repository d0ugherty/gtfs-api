using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using GtfsApi.Models;
using Calendar = GtfsApi.Models.Calendar;
using InvalidOperationException = System.InvalidOperationException;
using Route = GtfsApi.Models.Route;

namespace DataImportUtility
{
    public class DataImportService(GtfsContext context)
    {
        private readonly List<string> _agencies = ["SEPTA", "Amtrak", "NJ Transit"];
        private readonly List<string> _modes = ["rail", "bus"];

        private void ImportAgency(string filePath, string agencyName)
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
                try
                {
                    var parentAgency = context.ParentAgencies.First(pa => pa.Name.Equals(agencyName));

                    int row = 1;
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        context.Agencies.Add(new Agency
                        {
                            AgencyId = record.agency_id.Trim(),
                            Name = record.agency_name.Trim(),
                            Url = record.agency_url,
                            Timezone = record.agency_timezone,
                            Language = record.agency_lang,
                            Email = record.agency_email,
                            Fk_parentAgencyId = parentAgency.Id,
                            ParentAgency = parentAgency
                        });
                        row++;
                    }

                    Console.WriteLine("Saving changes...");
                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                      $"{filePath} \n " +
                                      $"{ex}");
                    throw;
                }
            }
        }

        private void ImportRoutes(string filePath, string agencyName)
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
                
                int row = 1;
                try
                {
                    var agencies = context.Agencies.OrderBy(agency => agency.Id);
                    var parentAgency = context.ParentAgencies.Single(pa => pa.Name.Equals(agencyName));
                    
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        var agencyId = record.agency_id;
                        var agency = agencies.LastOrDefault(a => a.AgencyId.Equals(agencyId)) 
                                     ?? context.Agencies.Add( new Agency
                                     {
                                         AgencyId = agencyId,
                                         Name = agencyId,
                                         Fk_parentAgencyId = parentAgency.Id,
                                         ParentAgency = parentAgency
                                     }).Entity;

                        context.Routes.Add(new Route
                        {
                            RouteId = record.route_id,
                            ShortName = record.route_short_name,
                            LongName = record.route_long_name,
                            Color = record.route_color,
                            TextColor = record.route_text_color,
                            Description = record.route_desc,
                            Type = record.route_type,
                            Url = record.route_url,
                            GtfsAgencyId = record.agency_id,
                            Agency = agency,
                            Fk_agencyId = agency.Id
                        });
                        row++;
                    }
                    Console.WriteLine("Saving changes...");

                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                      $"{filePath} \n " + 
                                      $"ROW: {row}\n" +
                                      $"{ex}");
                    throw;
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
                
                try
                {
                    int row = 1;
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        context.Calendars.Add(new Calendar
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
                        row++;
                    }
                    Console.WriteLine("Saving changes...");

                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
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
                try
                {
                    int row = 1;
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        context.CalendarDates.Add(new CalendarDate
                        {
                            ServiceId = record.service_id,
                            Date = record.date,
                            ExceptionType = record.exception_type
                        });
                        row++;
                    }
                    Console.WriteLine("Saving changes...");

                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
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
                
                try
                {
                    int row = 1;
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        context.Stops.Add(new Stop
                        {
                            StopId = record.stop_id,
                            Name = record.stop_name,
                            Description = record.stop_desc,
                            Latitude = record.stop_lat,
                            Longitude = record.stop_lon,
                            ZoneId = record.zone_id?.Trim(),
                            Url = record.stop_url,
                        });
                        row++;
                    }
                    Console.WriteLine("Saving changes...");

                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
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
                
                try
                {
                    int row = 1;
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        context.Shapes.Add(new Shape
                        {
                            ShapeId = record.shape_id,
                            ShapePtLat = record.shape_pt_lat,
                            ShapePtLon = record.shape_pt_lon,
                            Sequence = record.shape_pt_sequence,
                            DistanceTraveled = record.shape_dist_traveled
                        });
                        row++;
                    }
                    Console.WriteLine("Saving changes...");

                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void ImportTrips(string filePath)
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

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var routes = context.Routes.OrderBy(route => route.Id);
                        
                        int row = 1;
                        foreach (var record in records)
                        {
                            Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                            Route route = routes.Last(route => route.RouteId.Equals(record.route_id));

                            context.Trips.Add(new Trip
                            {
                                ServiceId = record.service_id,
                                TripId = record.trip_id,
                                Headsign = record.trip_headsign,
                                BlockId = record.block_id,
                                ShortName = record.trip_short_name,
                                DirectionId = record.direction_id,
                                GtfsRouteId = record.route_id,
                                Route = route,
                                Fk_routeId = route.Id,
                                ShapeId = record.shape_id
                            });
                            row++;
                        }

                        context.SaveChanges();
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
                try
                { ;
                    // Preloading for Stops
                    var latestStops = context.Stops
                        .GroupBy(s => s.StopId)
                        .Select(g => g.OrderByDescending(stop => stop.Id).FirstOrDefault())
                        .ToList();

                    var stopLookup = latestStops.ToDictionary(stop => stop.StopId, stop => stop);

                    // Preloading for Trips
                    var latestTrips = context.Trips
                        .GroupBy(trip => trip.TripId)
                        .Select(g => g.OrderByDescending(trip => trip.Id).FirstOrDefault())
                        .ToList();

                    var tripLookup = latestTrips.ToDictionary(trip => trip.TripId, trip => trip);

                    
                    int row = 1;
                    foreach (var record in records)
                    {
                        {
                            Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                            
                            if (stopLookup.TryGetValue(record.stop_id, out var stop) &&
                                tripLookup.TryGetValue(record.trip_id, out var trip))
                            {
                                context.StopTimes.Add(new StopTime
                                {
                                    ArrivalTime = record.arrival_time,
                                    DepartureTime = record.departure_time,
                                    StopSequence = record.stop_sequence,
                                    PickupType = record.pickup_type,
                                    DropoffType = record.drop_off_type,
                                    StopId = record.stop_id,
                                    TripId = record.trip_id,
                                    Stop = stop ?? throw new InvalidOperationException(),
                                    Fk_stopId = stop.Id,
                                    Trip = trip ?? throw new InvalidOperationException(),
                                    Fk_tripId = trip.Id,
                                });
                            }
                            else
                            {
                                Console.WriteLine($"Stop {record.stop_id} or Trip {record.trip_id} not found");
                            }

                            row++;
                        }
                    }                    
                    Console.WriteLine("Saving changes...");

                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                      $"{filePath} \n " +
                                      $"{ex}");
                    throw;
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
                
                try
                {
                    int row = 1;
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        context.Fares.Add(new Fare
                        {
                            FareId = record.fare_id,
                            OriginId = record.origin_id.Trim(),
                            DestinationId = record.destination_id.Trim()
                        });
                        row++;
                    }
                    Console.WriteLine("Saving changes...");

                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
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
            try
                {
                    int row = 1;
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        var fareId = record.fare_id;
                        var fare = context.Fares.SingleOrDefault(f => f.FareId == fareId)
                                   ?? context.Fares.Add(new Fare { FareId = fareId }).Entity;

                        context.FareAttributesTbl.Add(new FareAttributes
                        {
                            Price = record.price,
                            CurrencyType = record.currency_type,
                            PaymentMethod = record.payment_method,
                            Transfers = record.transfers,
                            TransferDuration = record.transfer_duration,
                            GtfsFareId = record.fare_id,
                            Fare = fare,
                            Fk_fareId = fare.Id
                        });
                        row++;
                    }
                    Console.WriteLine("Saving changes...");

                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                      $"{filePath} \n " +
                                      $"{ex}");
                    throw;
                }
            }
        }
    

        private void ImportFeedInfo(string filePath)
        {
            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                MissingFieldFound = null
            });
            csv.Read();
            csv.ReadHeader();

            var records = csv.GetRecords<FeedInfoCsv>();
            
            try
            {
                var agencies = context.Agencies.OrderBy(agency => agency.Id);
                
                int row = 1;
                foreach (var record in records)
                {
                    Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                    string publisherName = record.feed_publisher_name.Trim();

                    Agency agency = agencies.Last(a => a.Name.Equals(publisherName));

                    context.FeedInfoTbl.Add(new FeedInfo
                    {
                        FeedPublisherName = publisherName,
                        FeedPublisherUrl = record.feed_publisher_url,
                        FeedLanguage = record.feed_lang,
                        FeedStartDate = record.feed_start_date,
                        FeedEndDate = record.feed_end_date,
                        FeedVersion = record.feed_version,
                        Fk_agencyId = agency.Id,
                        Agency = agency
                    });
                    row++;
                }                    
                Console.WriteLine("Saving changes...");
                context.SaveChanges();
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Invalid operation occurred during importing of \n " +
                                  $"{filePath} \n " +
                                  $"{ex}");
                throw;
            }
        }

        private void ImportTransfers(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();

                var records = csv.GetRecords<TransfersCsv>();
                try
                {
                    var stops = context.Stops.OrderBy(stop => stop.Id);
                    int row = 1;
                    foreach (var record in records)
                    {
                        Console.Write($"{new string(' ', 20)}Importing row {row}\r");
                        string fromStopId = record.from_stop_id;
                        string toStopId = record.to_stop_id;

                        // Figure out a faster way to do this
                        Stop fromStop = stops.Last(stop => stop.StopId == fromStopId);

                        Stop toStop = stops.Last(stop => stop.StopId == toStopId);

                        context.Transfers.Add(new Transfer
                        {
                            FromStopId = fromStopId,
                            ToStopId = toStopId,
                            TransferType = record.transfer_type,
                            MinTransferTime = record.min_transfer_time,
                            FromStop = fromStop,
                            Fk_fromStopId = fromStop.Id,
                            ToStop = toStop,
                            Fk_toStopId = toStop.Id
                        });
                        row++;
                    }
                    Console.WriteLine("Saving changes...");
                    context.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private void ImportTry(string filePath, Action<string> import)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    Console.WriteLine($"Importing {filePath}...");
                    import(filePath);
                    Console.WriteLine($"{filePath} \n successfully imported.");
                }
                catch (ReaderException ex)
                {
                    Console.WriteLine($"No big deal: {ex}");
                    Console.WriteLine("Reader exception occurred, moving on to next import.");
                }
            }
            else
            {
                Console.WriteLine($"{filePath} does not exist. \n Moving on to next import.");
            }
        }

        private void ImportParentAgencies(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();

                var records = csv.GetRecords<ParentAgenciesCsv>();
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            context.ParentAgencies.Add(new ParentAgency
                            {
                                Name = record.name
                            });
                        }

                        context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (InvalidOperationException ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine(ex);
                    }
                }
            }
        }

    public void ImportData(string data="all")
    {
        ImportTry("../../data/ParentAgencies.csv", ImportParentAgencies);
        
            foreach (var agency in _agencies)
            {
                foreach (var mode in _modes)
                {
                    ImportTry($"../../data/{agency}/{mode}/agency.csv", filePath => ImportAgency(filePath, agency));

                    ImportTry($"../../data/{agency}/{mode}/routes.csv", filePath => ImportRoutes(filePath, agency));

                    ImportTry($"../../data/{agency}/{mode}/calendar.csv", ImportCalendar);

                    ImportTry($"../../data/{agency}/{mode}/calendar_dates.csv", ImportCalendarDates);

                    ImportTry($"../../data/{agency}/{mode}/stops.csv", ImportStops);

                    ImportTry($"../../data/{agency}/{mode}/trips.csv", ImportTrips);

                    ImportTry($"../../data/{agency}/{mode}/stop_times.csv", ImportStopTimes);

                    ImportTry($"../../data/{agency}/{mode}/shapes.csv", ImportShapes);
                    
                    ImportTry($"../../data/{agency}/{mode}/fare_rules.csv", ImportFares);

                    ImportTry($"../../data/{agency}/{mode}/fare_attributes.csv", ImportFareAttributes);
                    
                    ImportTry($"../../data/{agency}/{mode}/feed_info.csv", ImportFeedInfo);

                    ImportTry($"../../data/{agency}/{mode}/transfers.csv", ImportTransfers);
                }
            }
        }
    }
}