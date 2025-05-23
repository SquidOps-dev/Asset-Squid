using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.DAL
{
    // Adapter to fetch device distribution report from Devices table
    public class ReportAdapter : IReportAdapter
    {
        // Path to the SQLite database file
        private string connectionString = @"Data Source=inventory.db";

        public ReportAdapter()
        {

            var dbFullPath = @"Data Source=inventory.db";
            connectionString = dbFullPath;
        }
        public List<Report> GetDeviceReport()
        {
            // Open sql connection
            using var connection = new SqliteConnection(connectionString);

            // SQL query to retrieve device details along with
            // the location name and the total number of devices.
            // Each row represents a device, joined with its location
            // (if assigned), and includes a TotalDevices field
            // that contains the count of all devices in the Devices table.
            string sql = @"SELECT d.DeviceId, d.Name AS DeviceName,
                        d.SerialNumber, d.IpAddress, l.Name AS LocationName,
                        (SELECT COUNT(*) FROM Devices) AS TotalDevices
                        FROM Devices d
                        LEFT JOIN Locations l ON d.LocationId = l.LocationId
                        ORDER BY d.DeviceId;";

            // Execute the query and map each row to the Report object.
            return connection.Query<Report>(sql).AsList();
        }
    }
}
