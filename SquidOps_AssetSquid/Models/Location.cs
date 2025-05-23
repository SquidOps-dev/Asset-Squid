using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Dapper;

namespace SquidOps_AssetSquid.Models
{
    /// <summary>
    /// Class to represent Locations
    /// </summary>
    public class Location
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
    }
}
