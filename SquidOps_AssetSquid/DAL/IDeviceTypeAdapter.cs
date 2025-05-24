using SquidOps_AssetSquid.Models;
using System.Collections.Generic;

namespace SquidOps_AssetSquid.DAL
{
    /// <summary>
    /// Defines methods for retrieving device type data.
    /// </summary>
    public interface IDeviceTypeAdapter
    {
        /// <summary>
        /// Returns all device types available in the database.
        /// </summary>
        IEnumerable<DeviceType> GetAll();
    }
}
