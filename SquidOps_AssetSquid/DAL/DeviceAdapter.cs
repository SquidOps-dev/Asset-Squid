using Dapper; // ORM for executing SQL queries and mapping results
using Microsoft.Data.Sqlite; // SQLite database provider
using SquidOps_AssetSquid.Models; // Device model definitions
using System.Collections.Generic; // For IEnumerable<T>

namespace SquidOps_AssetSquid.DAL
{
    /// <summary>
    /// Performs Create, Read, Update, Delete operations for Device entities using SQLite.
    /// </summary>
    public class DeviceAdapter : IDeviceAdapter
    {
        // Connection string pointing to the local SQLite database file
        private readonly string connectionString;

        /// <summary>
        /// Default constructor initializes the connection string.
        /// </summary>
        public DeviceAdapter()
        {
            // Set the path to the SQLite database
            connectionString = @"Data Source=inventory.db";
        }

        /// <summary>
        /// Retrieves all Device records from the Devices table.
        /// </summary>
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

        /// <summary>
        /// Retrieves a single Device by its ID, or null if not found.
        /// </summary>
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

        /// <summary>
        /// Inserts a new Device record into the database.
        /// Returns true if at least one row was affected.
        /// </summary>
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

        /// <summary>
        /// Updates an existing Device record based on its DeviceId.
        /// Returns true if at least one row was affected.
        /// </summary>
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

        /// <summary>
        /// Deletes a Device record by its ID.
        /// Returns true if at least one row was affected.
        /// </summary>
        public bool DeleteDeviceById(int id)
        {
            const string sql = "DELETE FROM Devices WHERE DeviceId = @DeviceId";

            using var connection = new SqliteConnection(connectionString);
            return connection.Execute(sql, new { DeviceId = id }) > 0;
        }

        /// <summary>
        /// Retrieves all Devices filtered by LocationId.
        /// </summary>
        public IEnumerable<Device> GetDevicesByLocation(int locationId)
        {
            const string sql = @"
                SELECT DeviceId, Name, SerialNumber, IpAddress, MacAddress,
                       DeviceModel, LocationId, DeviceTypeId
                FROM Devices
                WHERE LocationId = @LocationId";

            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Device>(sql, new { LocationId = locationId });
        }

        /// <summary>
        /// Retrieves all Devices filtered by DeviceTypeId.
        /// </summary>
        public IEnumerable<Device> GetDevicesByDeviceType(int typeId)
        {
            const string sql = @"
                SELECT DeviceId, Name, SerialNumber, IpAddress, MacAddress,
                       DeviceModel, LocationId, DeviceTypeId
                FROM Devices
                WHERE DeviceTypeId = @DeviceTypeId";

            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Device>(sql, new { DeviceTypeId = typeId });
        }

        /// <summary>
        /// Searches Devices by matching the keyword against multiple fields.
        /// </summary>
        public IEnumerable<Device> SearchDevices(string keyword)
        {
            const string sql = @"
                SELECT DeviceId, Name, SerialNumber, IpAddress, MacAddress,
                       DeviceModel, LocationId, DeviceTypeId
                FROM Devices
                WHERE Name LIKE @Keyword
                   OR SerialNumber LIKE @Keyword
                   OR IpAddress LIKE @Keyword
                   OR MacAddress LIKE @Keyword
                   OR DeviceModel LIKE @Keyword";

            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Device>(sql, new { Keyword = $"%{keyword}%" });
        }

        /// <summary>
        /// Retrieves all Devices with joined LocationName and DeviceTypeName.
        /// </summary>
        public IEnumerable<Device> GetAllWithDetails()
        {
            const string sql = @"
                SELECT d.*, l.Name AS LocationName, t.TypeName AS DeviceTypeName
                FROM Devices d
                LEFT JOIN Locations l ON d.LocationId = l.LocationId
                LEFT JOIN Device_Type t ON d.DeviceTypeId = t.DeviceTypeId";

            using var connection = new SqliteConnection(connectionString);
            return connection.Query<Device>(sql);
        }
    }
}
