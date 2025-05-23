using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.DAL
{
    public interface ILocationAdapter
    {
        bool DeleteLocationById(int id);
        IEnumerable<Location> GetAll();
        Location GetById(int id);
        bool InsertLocation(Location location);
        bool UpdateLocation(Location location);
    }
}