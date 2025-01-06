namespace Domain.DataModel
{
	public class Location
	{
		public int Id { get; set; }
		public Guid UserId { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string TimeZone { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
