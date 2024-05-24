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
		    
            importService.ImportData();
		    
            Console.WriteLine("Data import completed successfully.");
        }
    }
}