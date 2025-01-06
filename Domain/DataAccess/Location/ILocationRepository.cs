using System.Runtime.CompilerServices;

namespace Domain.DataAccess.Location
{
	public interface ILocationRepository : ILocationReaderRepository
	{
		int AddLocation(DataModel.Location location);
	}
}
