using Dapper;
using Microsoft.Data.Sqlite;
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
        private readonly string connectionString = @"Data Source=inventory.db";

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

        public IEnumerable<Device> GetAllWithDetails()
        {
            using var connection = new SqliteConnection(connectionString);
            var sql = @"
                        SELECT 
                            d.*, 
                            l.Name AS LocationName,
                            t.TypeName AS DeviceTypeName
                        FROM Devices d
                        LEFT JOIN Locations l ON d.LocationId = l.LocationId
                        LEFT JOIN Device_Type t ON d.DeviceTypeId = t.DeviceTypeId";

            return connection.Query<Device>(sql);
        }

    }
}
