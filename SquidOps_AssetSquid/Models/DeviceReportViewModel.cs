namespace SquidOps_AssetSquid.Models
{
    /// <summary>
    /// Class to represent a report
    /// </summary>
    public class DeviceReportViewModel
    {
        public int DeviceId { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string DeviceModel { get; set; }
        public string DeviceTypeName { get; set; }
        public string LocationName { get; set; }
        public int TotalDevices { get; set; }
    }
}
