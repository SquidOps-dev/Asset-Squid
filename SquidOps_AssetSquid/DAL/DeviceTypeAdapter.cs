using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.DAL
{
    public class DeviceTypeAdapter : IDeviceTypeAdapter
    {
        private readonly string connectionString = @"Data Source=inventory.db";

        public DeviceTypeAdapter()
        {

            var dbFullPath = @"Data Source=inventory.db";
            connectionString = dbFullPath;
        }
        public IEnumerable<DeviceType> GetAll()
        {
            const string sql = "SELECT DeviceTypeId, TypeName FROM Device_Type";

            using var connection = new SqliteConnection(connectionString);
            return connection.Query<DeviceType>(sql);
        }
    }
}
