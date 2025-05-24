using SquidOps_AssetSquid.Models;
using System.Collections.Generic;

namespace SquidOps_AssetSquid.DAL
{
    /// <summary>
    /// Defines methods for CRUD operations on Location entities.
    /// </summary>
    public interface ILocationAdapter
    {
        /// <summary>
        /// Deletes the location with the specified ID; returns true if deletion succeeded.
        /// </summary>
        bool DeleteLocationById(int id);

        /// <summary>
        /// Retrieves all locations from the database.
        /// </summary>
        List<Location> GetAll();

        /// <summary>
        /// Retrieves a single Location by its ID; returns null if not found.
        /// </summary>
        Location GetById(int id);

        /// <summary>
        /// Inserts a new Location record; returns true if insertion succeeded.
        /// </summary>
        bool InsertLocation(Location location);

        /// <summary>
        /// Updates an existing Location record; returns true if update succeeded.
        /// </summary>
        bool UpdateLocation(Location location);
    }
}

