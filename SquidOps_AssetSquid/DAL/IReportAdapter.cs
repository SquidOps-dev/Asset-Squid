using SquidOps_AssetSquid.Models;

namespace SquidOps_AssetSquid.DAL
{
    public interface IReportAdapter
    {
        List<Report> GetDeviceReport();
    }
}
