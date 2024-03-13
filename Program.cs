using Microsoft.EntityFrameworkCore;
using GtfsApi.Models;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Database Context
builder.Services.AddDbContext<GtfsContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("GtfsApiDatabase") ?? "Data Source = gtfs.db"));

builder.Services.AddHttpLogging(
    opts => opts.LoggingFields = HttpLoggingFields.RequestProperties);

builder.Logging.AddFilter(
    "Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
