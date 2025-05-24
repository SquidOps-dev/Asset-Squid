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
        IEnumerable<Device> GetDevicesByLocation(int locationId);
        IEnumerable<Device> GetDevicesByDeviceType(int typeId);
        IEnumerable<Device> SearchDevices(string keyword);
        IEnumerable<Device> GetAllWithDetails();
    }
}
