namespace Domain.DataAccess.Location
{
	public interface ILocationRepository : ILocationReaderRepository
	{
		void AddLocations(IEnumerable<DataModel.Location> locations);
	}
}
