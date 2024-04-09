using SpeedSight.Dto;
using SpeedSight.Models;

namespace SpeedSight.Interfaces
{
    public interface IGpsDataRepository
    {
        ICollection<GpsData> GetGpsDatas();
        GpsData GetGpsData(int id);
        double GetAvgSpeedForId(int id);
        Dictionary<int,double> GetAvgSpeedForAll();
        bool GpsDataExists(int id);
        bool CreateGpsData(GpsData data);
        bool UpdateGpsData(GpsData data);
        bool DeleteGpsData(GpsData data);
        bool Save();
    }
}
