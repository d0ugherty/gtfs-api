using Microsoft.EntityFrameworkCore;
using GtfsApi.Models;
using RouteContext = GtfsApi.Models.RouteContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Database Context
builder.Services.AddDbContext<AgencyContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<RouteContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<StopContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<StopTimeContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<TripContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<ShapeContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<CalendarContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<CalendarDateContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<FareContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));

builder.Services.AddDbContext<FareAttributesContext>(opt =>
    opt.UseInMemoryDatabase("GtfsData"));



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
