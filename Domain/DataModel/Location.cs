namespace Domain.DataModel
{
	public class Location
	{
		public Location()
		{
			Guid = Guid.NewGuid();
		}

		public Guid Guid { get; set; }
		public Guid UserId { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string TimeZone { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
