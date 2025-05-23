using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.DAL
{
    public interface IDeviceAdapter
    {
        IEnumerable<Device> GetAll();
        Device GetById(int id);
        bool InsertDevice(Device device);
        bool UpdateDevice(Device device);
        bool DeleteDeviceById(int id);
    }
}
