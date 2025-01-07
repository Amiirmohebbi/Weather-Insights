using DataAccess;
using DataAccess.DatabaseHelper;
using Microsoft.Data.SqlClient;
using Service;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(new DbInitializer(
		builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Logging.AddEventLog();

builder.Services.AddServiceServices();
builder.Services.AddDataAccessServices();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var dataContext = services.GetRequiredService<DbInitializer>();
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
