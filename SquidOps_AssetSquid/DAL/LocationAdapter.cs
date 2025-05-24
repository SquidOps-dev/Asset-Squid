using Microsoft.Data.Sqlite;
using Dapper;
using SquidOps_AssetSquid.Models;
using System.Collections.Generic;
using System.Linq;

namespace SquidOps_AssetSquid.DAL
{
    /// <summary>
    /// Implements ILocationAdapter to manage Location records in SQLite.
    /// </summary>
    public class LocationAdapter : ILocationAdapter
    {
        // Connection string pointing to the SQLite database file (inventory.db)
        private string connectionString = @"Data Source=inventory.db";

        /// <summary>
        /// Default constructor sets the connection string (identical to field initializer).
        /// </summary>
        public LocationAdapter()
        {
            var dbFullPath = @"Data Source=inventory.db"; // Path to DB file
            connectionString = dbFullPath;
        }

        /// <summary>
        /// Retrieves all Locations: SELECT LocationId and Name.
        /// </summary>
        public List<Location> GetAll()
        {
            const string sql = @"
                SELECT LocationId, Name
                FROM Locations";

            using (var connection = new SqliteConnection(connectionString))
            {
                // Execute the query and convert the results to a List<Location>
                return connection.Query<Location>(sql).ToList();
            }
        }

        /// <summary>
        /// Retrieves a single Location by ID.
        /// </summary>
        public Location GetById(int id)
        {
            const string sql = @"
                SELECT LocationId, Name
                FROM Locations
                WHERE LocationId = @LocationId";

            using var connection = new SqliteConnection(connectionString);
            // QueryFirst returns the first row or throws; adjust if you want null instead
            return connection.QueryFirst<Location>(sql, new { LocationId = id });
        }

        /// <summary>
        /// Inserts a new Location record into the table.
        /// </summary>
        public bool InsertLocation(Location location)
        {
            const string sql = @"
                INSERT INTO Locations (Name)
                VALUES (@Name)";

            using var connection = new SqliteConnection(connectionString);
            // Execute returns number of rows affected
            int rows = connection.Execute(sql, location);
            return rows > 0; // Return true if at least one row inserted
        }

        /// <summary>
        /// Updates an existing Location record by ID.
        /// </summary>
        public bool UpdateLocation(Location location)
        {
            const string sql = @"
                UPDATE Locations
                SET Name = @Name
                WHERE LocationId = @LocationId";

            using var connection = new SqliteConnection(connectionString);
            // Returns true if at least one row was updated
            return connection.Execute(sql, location) > 0;
        }

        /// <summary>
        /// Deletes a Location record matching the specified ID.
        /// </summary>
        public bool DeleteLocationById(int id)
        {
            const string sql = @"
                DELETE FROM Locations
                WHERE LocationId = @LocationId";

            using var connection = new SqliteConnection(connectionString);
            // Execute the delete and check number of rows affected
            int rows = connection.Execute(sql, new { LocationId = id });
            return rows > 0; // Return true if delete succeeded
        }
    }
}
