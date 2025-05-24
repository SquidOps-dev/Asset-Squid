using Microsoft.Data.Sqlite;
using Dapper;
using SquidOps_AssetSquid.Models;
using System.Collections.Generic;

namespace SquidOps_AssetSquid.DAL
{
    /// <summary>
    /// Adapter for retrieving device type data from the SQLite database.
    /// </summary>
    public class DeviceTypeAdapter : IDeviceTypeAdapter
    {
        // Connection string specifying the SQLite database file
        private readonly string connectionString = @"Data Source=inventory.db";

        /// <summary>
        /// Default constructor reassigns the database connection string (currently redundant).
        /// </summary>
        public DeviceTypeAdapter()
        {
            var dbFullPath = @"Data Source=inventory.db"; // Local path to the DB file
            connectionString = dbFullPath;
        }

        /// <summary>
        /// Retrieves all device types from the Device_Type table.
        /// </summary>
        public IEnumerable<DeviceType> GetAll()
        {
            const string sql = "SELECT DeviceTypeId, TypeName FROM Device_Type"; // SQL query

            using var connection = new SqliteConnection(connectionString); // Open a new DB connection
            return connection.Query<DeviceType>(sql); // Execute query and map results to DeviceType objects
        }
    }
}
