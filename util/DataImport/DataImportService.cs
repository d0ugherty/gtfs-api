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
        private readonly List<string> _agencies = ["septa", "njt"];
        private readonly List<string> _modes = ["rail", "bus"];

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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            context.Agencies.Add(new Agency
                            {
                                AgencyId = record.agency_id.ToUpper(),
                                Name = record.agency_name.ToUpper(),
                                Url = record.agency_url,
                                Timezone = record.agency_timezone,
                                Language = record.agency_lang,
                                Email = record.agency_email
                            });
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            var agencyId = record.agency_id.ToUpper();
                            var agency = context.Agencies.SingleOrDefault(a => a.AgencyId.Equals(agencyId)) 
                                         ?? context.Agencies.Add(new Agency { AgencyId = agencyId, Name = agencyId }).Entity ;

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
                                FkAgencyId = agency.Id
                            });
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
                }

                context.SaveChanges();
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
                    context.CalendarDates.Add(new CalendarDate
                    {  
                        ServiceId = record.service_id,
                        Date = record.date,
                        ExceptionType = record.exception_type
                    });
                }

                context.SaveChanges();
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
                    context.Stops.Add(new Stop
                    {  
                        StopId = record.stop_id,
                        Name = record.stop_name,
                        Description = record.stop_desc,
                        Latitude = record.stop_lat,
                        Longitude = record.stop_lon,
                        ZoneId = record.zone_id?.Trim(),
                        Url = record.stop_url
                    });
                }

                context.SaveChanges();
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
                    context.Shapes.Add(new Shape
                    {
                        ShapeId = record.shape_id,
                        ShapePtLat = record.shape_pt_lat,
                        ShapePtLon = record.shape_pt_lon,
                        Sequence = record.shape_pt_sequence,
                        DistanceTraveled = record.shape_dist_traveled
                    });
                }

                context.SaveChanges();
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var agency = context.Agencies.FirstOrDefault(ag => ag.AgencyId.Equals(agencyId.ToUpper()));
                        if (agency == null) throw new InvalidOperationException("Agency not found.");
                        
                        var routesDictionary = context.Routes
                            .Where(rt => rt.FkAgencyId == agency.Id)
                            .ToDictionary(rt => rt.RouteId, rt => rt);

                        foreach (var record in records)
                        {
                            if (!routesDictionary.TryGetValue(record.route_id, out var route))
                            {
                                route = context.Routes.Add(new Route
                                {
                                    RouteId = record.route_id,
                                    Agency = agency,
                                    FkAgencyId = agency.Id,
                                    GtfsAgencyId = agency.AgencyId
                                }).Entity;
                              
                                routesDictionary[record.route_id] = route;
                            }

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
                                FkRouteId = route.Id,
                                ShapeId = record.shape_id
                            });
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

        /**
         *  TO DO: Optimize this.
         *
         */
        private void ImportStopTimes(string filePath, string agencyName, string mode)
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            var stop = context.Stops.FirstOrDefault(st => st.StopId == record.stop_id);

                            var trip = context.Trips.FirstOrDefault(tr => tr.TripId == record.trip_id);

                            context.StopTimes.Add(new StopTime
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
                    context.Fares.Add(new Fare
                    {
                       FareId = record.fare_id,
                       OriginId = record.origin_id.Trim(),
                       DestinationId = record.destination_id.Trim()
                    });
                }
                context.SaveChanges();
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
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
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
                                FkFareId = fare.Id
                            });
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

        private void ImportFeedInfo(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();

                var records = csv.GetRecords<FeedInfoCsv>();

                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            string publisherName = record.feed_publisher_name.Trim();

                            Agency agency = context.Agencies.FirstOrDefault(a => a.Name.Equals(publisherName))
                                            ?? context.Agencies.First(a => a.AgencyId.Equals(publisherName));

                            context.FeedInfoTbl.Add(new FeedInfo
                            {
                                FeedPublisherName = publisherName,
                                FeedPublisherUrl = record.feed_publisher_url,
                                FeedLanguage = record.feed_lang,
                                FeedStartDate = record.feed_start_date,
                                FeedEndDate = record.feed_end_date,
                                FeedVersion = record.feed_version,
                                FkAgencyId = agency.Id,
                                Agency = agency
                            });

                            context.SaveChanges();
                            transaction.Commit();
                        }
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

        private void ImportTransfers(string filePath, string agency, string mode)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                   {
                       MissingFieldFound = null
                   }))
            {
                csv.Read();
                csv.ReadHeader();

                var agencyId = agency;
                
                if (agency.Equals("septa") && mode.Equals("bus"))
                {
                    agencyId = "1";
                }
                
                List<Route> routes = context.Routes
                    .Where(rt => rt.GtfsAgencyId.Equals(agencyId.ToUpper()))
                    .ToList();
                
                List<int> routeIds = routes
                    .Select(route => route.Id)
                    .ToList();
                
                List<Trip> trips = context.Trips
                    .Where(trip => routeIds.Contains(trip.FkRouteId))
                    .ToList();
                
                List<int> tripIds = trips.Select(trip => trip.Id).ToList();

                List<StopTime?> stopTimes = context.StopTimes
                    .Where(stopTime => tripIds.Contains(stopTime.FkTripId))
                    .GroupBy(stopTime => stopTime.FkStopId)
                    .Select(stopId => stopId.FirstOrDefault())
                    .ToList();
                
                List<int> agencyStopIds = stopTimes.Select(stopTime => stopTime!.FkStopId).ToList();
                
                List<Stop> stops = context.Stops
                    .Where(stop => agencyStopIds.Contains(stop.Id))
                    .Select(stop => stop)
                    .ToList();

                foreach (var stop in stops)
                {
                    Console.WriteLine(stop.StopId);
                }

                var records = csv.GetRecords<TransfersCsv>();
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        foreach (var record in records)
                        {
                            int fromStopId = record.from_stop_id;
                            int toStopId = record.to_stop_id;

                            Stop fromStop = stops.First(stop => stop.StopId == fromStopId);

                            Stop toStop = stops.First(stop => stop.StopId == toStopId);

                            context.Transfers.Add(new Transfer
                            {
                                FromStopId = fromStopId,
                                ToStopId = toStopId,
                                TransferType = record.transfer_type,
                                MinTransferTime = record.min_transfer_time,
                                FromStop = fromStop,
                                FkFromStopId = fromStop.Id,
                                ToStop = toStop,
                                FkToStopId = toStop.Id
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
            foreach (var agency in _agencies)
            {
                foreach (var mode in _modes)
                {
                    ImportTry($"../../data/{agency}_{mode}/agency.csv", ImportAgency);

                    ImportTry($"../../data/{agency}_{mode}/routes.csv", ImportRoutes);

                    ImportTry($"../../data/{agency}_{mode}/calendar.csv", ImportCalendar);

                    ImportTry($"../../data/{agency}_{mode}/calendar_dates.csv", ImportCalendarDates);

                    ImportTry($"../../data/{agency}_{mode}/stops.csv", ImportStops);

                    ImportTry($"../../data/{agency}_{mode}/shapes.csv", ImportShapes);

                    ImportTry($"../../data/{agency}_{mode}/trips.csv", 
                        filePath => ImportTrips(filePath, agency));

                    ImportTry($"../../data/{agency}_{mode}/stop_times.csv", filePath => ImportStopTimes(filePath, agency, mode));

                    ImportTry($"../../data/{agency}_{mode}/fare_rules.csv", ImportFares);

                    ImportTry($"../../data/{agency}_{mode}/fare_attributes.csv", ImportFareAttributes);
                    
                    ImportTry($"../../data/{agency}_{mode}/feed_info.csv", ImportFeedInfo);

                    ImportTry($"../../data/{agency}_{mode}/transfers.csv",
                        filePath => ImportTransfers(filePath, agency, mode));
                }

            }
        }
    }

}