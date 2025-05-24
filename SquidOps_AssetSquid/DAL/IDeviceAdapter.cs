using SquidOps_AssetSquid.Models; // Device model definitions
using System.Collections.Generic; // For IEnumerable<T>

namespace SquidOps_AssetSquid.DAL
{
    /// <summary>
    /// Defines methods for CRUD and query operations on Device entities.
    /// </summary>
    public interface IDeviceAdapter
    {
        /// <summary>Returns all devices in the database.</summary>
        IEnumerable<Device> GetAll();

        /// <summary>Retrieves a device by its unique identifier.</summary>
        Device GetById(int id);

        /// <summary>Inserts a new device record; returns true if successful.</summary>
        bool InsertDevice(Device device);

        /// <summary>Updates an existing device; returns true if at least one row was affected.</summary>
        bool UpdateDevice(Device device);

        /// <summary>Deletes a device by ID; returns true if successful.</summary>
        bool DeleteDeviceById(int id);

        /// <summary>Gets all devices associated with a given location ID.</summary>
        IEnumerable<Device> GetDevicesByLocation(int locationId);

        /// <summary>Gets all devices of a specific type ID.</summary>
        IEnumerable<Device> GetDevicesByDeviceType(int typeId);

        /// <summary>Searches devices by matching keyword in several fields.</summary>
        IEnumerable<Device> SearchDevices(string keyword);

        /// <summary>
        /// Retrieves all devices along with related LocationName and DeviceTypeName.
        /// </summary>
        IEnumerable<Device> GetAllWithDetails();
    }
}
