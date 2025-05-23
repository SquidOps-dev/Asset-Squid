using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.DAL
{
    /// <summary>
    /// Handles database operations for Device entities.
    /// Adapter for Device CRUD operations using Dapper and SQLite.
    /// </summary>
    public class DeviceAdapter : IDeviceAdapter
    {
        // SQLite connection string; points to the inventory.db file
        // in the root of the program folder.
        private readonly string connectionString;

        public DeviceAdapter()
        {

            var dbFullPath = @"Data Source=inventory.db";
            connectionString = dbFullPath;
        }

        // Retrieves all device records from the Devices table.
        public IEnumerable<Device> GetAll()
        {
            const string sql = @"
                SELECT DeviceId, Name, SerialNumber, 
                       IpAddress, MacAddress, DeviceModel, 
                       LocationId, DeviceTypeId
                FROM Devices";

            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Device>(sql);
        }

        // Retrieves a specific device by its ID.
        public Device GetById(int id)
        {
            const string sql = @"
                SELECT DeviceId, Name, SerialNumber, 
                       IpAddress, MacAddress, DeviceModel, 
                       LocationId, DeviceTypeId
                FROM Devices
                WHERE DeviceId = @DeviceId";

            using var connection = new SqliteConnection(connectionString);
            return connection.QueryFirstOrDefault<Device>(sql, new { DeviceId = id });
        }

        // Inserts a new device record into the database.
        public bool InsertDevice(Device device)
        {
            const string sql = @"
                INSERT INTO Devices 
                    (Name, SerialNumber, IpAddress, MacAddress, DeviceModel, LocationId, DeviceTypeId)
                VALUES 
                    (@Name, @SerialNumber, @IpAddress, @MacAddress, @DeviceModel, @LocationId, @DeviceTypeId)";

            using var connection = new SqliteConnection(connectionString);
            return connection.Execute(sql, device) > 0;
        }

        // Updates an existing device in the database.
        public bool UpdateDevice(Device device)
        {
            const string sql = @"
                UPDATE Devices SET 
                    Name = @Name,
                    SerialNumber = @SerialNumber,
                    IpAddress = @IpAddress,
                    MacAddress = @MacAddress,
                    DeviceModel = @DeviceModel,
                    LocationId = @LocationId,
                    DeviceTypeId = @DeviceTypeId
                WHERE DeviceId = @DeviceId";

            using var connection = new SqliteConnection(connectionString);
            return connection.Execute(sql, device) > 0;
        }

        
        // Deletes a device record by its ID.
        public bool DeleteDeviceById(int id)
        {
            const string sql = "DELETE FROM Devices WHERE DeviceId = @DeviceId";

            using var connection = new SqliteConnection(connectionString);
            return connection.Execute(sql, new { DeviceId = id }) > 0;
        }
    }
}
