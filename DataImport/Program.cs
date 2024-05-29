using Gtfs.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataImport
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = "Data Source=../Gtfs.DataAccess/gtfs.db";
		    
            var optionsBuilder = new DbContextOptionsBuilder<GtfsContext>();
		    
            optionsBuilder.UseSqlite(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
		    
            var options = optionsBuilder.Options;

            var importService = new DataImport(new GtfsContext(options));

            string sourceName = "";
            
            if (args.Length > 0)
            {
                sourceName = args[0];
            }

            importService.ImportGtfsData(sourceName);
		    
            Console.WriteLine("Data import completed successfully.");
        }
    }
}