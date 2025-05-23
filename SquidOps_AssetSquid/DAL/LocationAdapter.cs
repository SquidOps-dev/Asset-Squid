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
    public class LocationAdapter : ILocationAdapter
    {
        // SQLite connection string; points to the inventory.db file
        // in the root of the program folder.
        private string connectionString = @"Data Source=inventory.db";

        public LocationAdapter()
        {

            var dbFullPath = @"Data Source=inventory.db";
            connectionString = dbFullPath;
        }

        // Retrieves all device records from the Locations table.
        public IEnumerable<Location> GetAll()
        {
            // SQL query selecting all columns for mapping to Location objects.
            const string sql = "SELECT LocationId, Name FROM Locations";

            // Create and open a new SQLite connection.
            using (SqliteConnection connection = new SqliteConnection(connectionString))
            {
                // Execute the query and map results to IEnumerable<Location>.
                return connection.Query<Location>(sql);
            }
        }
        // Retrieves a specific device by its ID.
        public Location GetById(int id)
        {
            // SQL query with parameter placeholder to prevent SQL injection.
            const string sql = @"SELECT LocationId, Name, FROM Locations WHERE 
                            LocationId = @LocationId";

            using var connection = new SqliteConnection(connectionString); // Open connection.

            // Execute the query, passing id into the parameter, and return first match.
            return connection.QueryFirst<Location>(sql, new { LocationId = id });
        }
        // Inserts a new device record into the database.
        public bool InsertLocation(Location location)
        {
            // SQL insert statement using named parameters matching Device properties.
            const string sql = @"INSERT INTO Locations 
                (Name) VALUES (@Name)";

            using (SqliteConnection connection = new SqliteConnection(connectionString)) // Open Connection
            {
                // Execute the insert; returns number of rows affected.
                int rowsAffected = connection.Execute(sql, location);
                if (rowsAffected > 0)
                {
                    // Return true if at least one row was inserted.
                    return true;
                }
                else
                {
                    // Return false if no rows were inserted.
                    return false;
                }
            }
        }
        // Updates an existing device from the Devices table.
        public bool UpdateLocation(Location location)
        {
            // SQL update statement with WHERE clause to target specific DeviceId.
            const string sql = @"UPDATE Locations SET LocationId= @LocationId, 
                        Name= @Name WHERE LocationId= @LocationId";

            // Open connection.
            using var connection = new SqliteConnection(connectionString);

            // Execute the update; returns number of rows affected.
            return connection.Execute(sql, location) > 0;
        }
        // Deletes an location record by its ID.
        public bool DeleteLocationById(int id)
        {
            // SQL delete statement using parameter to specify which record to remove.
            const string sql = "DELETE FROM Locations WHERE LocationId = @LocationId";

            using var connection = new SqliteConnection(connectionString); // Open connection.
            // Execute delete; returns number of rows affected.
            int rowsAffected = connection.Execute(sql, new { LocationId = id });
            // Return true if deletion succeeded.
            return rowsAffected > 0;
        }
    }
}
