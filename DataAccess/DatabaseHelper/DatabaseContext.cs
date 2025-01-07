using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataAccess.DatabaseHelper
{
	public class DatabaseContext
	{
		private readonly string connectionString;

		public DatabaseContext(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public IDbConnection CreateConnection()
		{
			return new SqlConnection(connectionString);
		}

		public async void Init()
		{
			if (!await IsInitCompleted())
			{
				using (var connection = CreateConnection())
				{


					string query = @"
                IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Locations')
                BEGIN
                    CREATE TABLE Locations (
										Guid UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()) PRIMARY KEY,
										UserId UNIQUEIDENTIFIER,
										Latitude FLOAT,
										Longitude FLOAT,
										TimeZone VARCHAR(25),
										CreatedDate DATETIME DEFAULT GETDATE());

                END

								IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WeatherDetails')
                BEGIN
										CREATE TABLE WeatherDetails (
										Guid UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()) PRIMARY KEY,
										LocationGuid UNIQUEIDENTIFIER,
										Time DATETIME,
										Temperature FLOAT,
										WeatherCode INT,
										WindSpeed FLOAT,
										IsDay BIT,
										Rain FLOAT,
										Showers FLOAT,
										Snowfall FLOAT,
										CloudCover INT,
										Pressure FLOAT,
										FOREIGN KEY (LocationGuid) REFERENCES Locations(Guid));

                END

								IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'WeatherDetailUnits')
                BEGIN
										CREATE TABLE WeatherDetailUnits (
										Guid UNIQUEIDENTIFIER NOT NULL DEFAULT(NEWID()) PRIMARY KEY,
										WeatherDetailGuid UNIQUEIDENTIFIER,
										Time NVARCHAR(15),
										Temperature NVARCHAR(15),
										WindSpeed NVARCHAR(15),
										Rain NVARCHAR(15),
										Showers NVARCHAR(15),
										Snowfall NVARCHAR(15),
										CloudCover NVARCHAR(15),
										Pressure NVARCHAR(15),
										FOREIGN KEY (WeatherDetailGuid) REFERENCES WeatherDetails(Guid));

                END

								IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Configuration')
								BEGIN
												CREATE TABLE Configuration (
										Id INT PRIMARY KEY IDENTITY(1,1),
										[Key] NVARCHAR(255) NOT NULL,
										Value NVARCHAR(255) NOT NULL);

								END";

					await connection.ExecuteAsync(query);
				} 

				CompleteInit();
			}
		}

		#region Private Methods
		private async Task<bool> IsInitCompleted()
		{
			bool result;
			
			using (var connection = CreateConnection())
			{
				string query = @"
							IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Configuration')
								BEGIN
										SELECT Value FROM Configuration WHERE [Key] = @Key
								END

							ELSE 
								BEGIN
										SELECT 0
								END";

				result = await connection.QuerySingleAsync<bool>(query, new { Key = "IsInitCompleted" });
			}

			return result;
		}

		private async void CompleteInit()
		{
			using (var connection = CreateConnection())
			{
				await connection.ExecuteAsync("INSERT INTO Configuration ([Key], Value) VALUES (@Key, @Value)", new { Key = "IsInitCompleted", Value = "True" });
			}
		} 
		#endregion
	}
}
