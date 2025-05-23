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
    /// Class to represent a report
    /// </summary>
    public class Report
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public string IpAddress { get; set; }
        public string LocationName { get; set; }
        public int TotalDevices { get; set; }
    }
}
