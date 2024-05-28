using Microsoft.EntityFrameworkCore;
using Gtfs.DataAccess;
using Gtfs.DataAccess.Repository;
using Gtfs.Domain.Interfaces;
using Gtfs.Domain.Services;
using Microsoft.AspNetCore.HttpLogging; // Ensure this matches the namespace where GtfsContext is defined

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddRazorPages();

// Database Context
builder.Services.AddDbContext<GtfsContext>(opt =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=../Gtfs.DataAccess/gtfs.db";
    opt.UseSqlite(connectionString,  x => x.MigrationsAssembly("Gtfs.DataAccess"));
    opt.EnableSensitiveDataLogging();
});

builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<DbContext, GtfsContext>();
builder.Services.AddScoped<IRepository<Route, int>, Repository<Route, int>>();
builder.Services.AddScoped<RouteService>();
builder.Services.AddScoped<AgencyService>();
builder.Services.AddScoped<StopService>();
builder.Services.AddScoped<TripService>();

builder.Services.AddHttpLogging(
    opts => opts.LoggingFields = HttpLoggingFields.RequestProperties);

builder.Logging.AddFilter(
    "Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseStaticWebAssets();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseHttpLogging();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();
app.MapRazorPages();
app.Run();