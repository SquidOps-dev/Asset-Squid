using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.DAL
{
    public interface IDeviceTypeAdapter
    {
        IEnumerable<DeviceType> GetAll();
    }
}