namespace SquidOps_AssetSquid.Models
{
    /// <summary>
    /// Class that represents Devices.
    /// </summary>
    public class Device
    {
        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string SerialNumber { get; set; }
        public string IpAddress { get; set; }
        public string MacAddress { get; set; }
        public string DeviceModel { get; set; }
        public string LocationName { get; set; }
        public string DeviceTypeName { get; set; }
        public int LocationId { get; set; }
        public int DeviceTypeId { get; set; }
    }
}
