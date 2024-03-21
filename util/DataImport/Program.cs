using Microsoft.EntityFrameworkCore;
using GtfsApi.Models;

namespace DataImportUtility
{
    class Program  
    {
	    static void Main(string[] args)
	    {
		    var databasePath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"../../../../../gtfs.db"));
		    var connectionString = $"Data Source={databasePath};";
		    
		    var optionsBuilder = new DbContextOptionsBuilder<GtfsContext>();
		    
		    optionsBuilder.UseSqlite(connectionString);
		    optionsBuilder.EnableSensitiveDataLogging();
		    
		    var options = optionsBuilder.Options;

		    var importService = new DataImportService(new GtfsContext(options));
		    
		    importService.ImportData();
		    
		    Console.WriteLine("Data import completed successfully.");
	    }
    }
}