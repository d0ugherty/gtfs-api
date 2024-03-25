using GtfsApi.Interfaces;
using Microsoft.EntityFrameworkCore;
using GtfsApi.Models;
using GtfsApi.Services;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRouteService, RouteService>();
builder.Services.AddScoped<IAgencyService, AgencyService>();
builder.Services.AddScoped<IStopService, StopService>();
builder.Services.AddScoped<IFareService, FareService>();
builder.Services.AddScoped<IFeedInfoService, FeedInfoService>();

builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddRazorPages();
// Database Context
builder.Services.AddDbContext<GtfsContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("GtfsApiDatabase") ?? "Data Source = gtfs.db");
    opt.EnableSensitiveDataLogging();
});

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
