using DataAccess.DatabaseHelper;
using DataAccess.Repository;
using Domain.DataAccess.Location;
using Domain.DataAccess.WeatherDetail;
using Domain.DataAccess.WeatherDetailUnits;
using Domain.ExternalService;
using Domain.Service;
using Microsoft.Data.SqlClient;
using Service;
using Service.ExternalService;
using Service.WeatherService;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new DatabaseContext(
		builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddScoped<IOpenMeteoService, OpenMeteoService>();
builder.Services.AddHostedService<WeatherForecastBackgroundWorker>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IWeatherManagerService, WeatherManagerService>();
builder.Services.AddScoped<IWeatherDetailRepository, WeatherDetailRepository>();
builder.Services.AddScoped<IWeatherDetailUnitsRepository, WeatherDetailUnitsRepository>();
builder.Services.AddSingleton<IWeatherForecastQueueService, WeatherForecastQueueService>();
builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Logging.AddEventLog();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var dataContext = services.GetRequiredService<DatabaseContext>();
	dataContext.Init();
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
