using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SpeedSight.Data;
using SpeedSight.Dto;
using SpeedSight.Interfaces;
using SpeedSight.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpeedSight.Repository
{
    public class GpsDataRepository : IGpsDataRepository
    {
        private readonly DataContext _context;

        public GpsDataRepository(DataContext context)
        {
            _context = context;
        }

        public GpsData GetGpsData(int id)
        {   
            return _context.GpsDatas.Where(p => p.Id == id).FirstOrDefault();
        }
        public ICollection<GpsData> GetGpsDatas() 
        {
            return _context.GpsDatas.OrderBy(p => p.Id).ToList();
        }

        public double GetAvgSpeedForLink(int id)
        {
            var data_id = _context.GpsDatas.Where(p => p.Id == id).FirstOrDefault();
            var data_link = _context.GpsDatas.Where(p => p.Link == data_id.Link);            

            if (data_link.Count() <= 0)
                return 0;
            
            return Math.Round((double)data_link.Sum(r => r.Match_Speed) / data_link.Count(),2);
        }

        public bool GpsDataExists(int id)
        {
            return _context.GpsDatas.Any(p => p.Id == id);
        }

        public bool CreateGpsData(GpsData data)
        {
            _context.Add(data);
            return Save();
        }

        public bool UpdateGpsData(GpsData data)
        {
            var link_validation= IsInRangeLink(data.Link);
            if (link_validation)
            {
                TimeSpan span = DateTime.Now.Subtract(new DateTime(1970,1,1,0,0,0));
                data.Utc = (int)(span.TotalSeconds);
                _context.Update(data);
                return Save();
            }
            return false;
        }

        public bool DeleteGpsData(GpsData data)
        {
            _context.Remove(data);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool IsInRangeLink(int link)
        {
            var validLink = Enum.GetValues(typeof(LinkTypes)).Cast<int>().OrderBy(x => x);
            return validLink.Contains(link);
        }

        public enum LinkTypes 
        {
            Link1 = 201678,
            Link2 = 201679,
            Link3 = 214697,
            Link4 = 214696,
            Link5 = 214695,
            Link6 = 214694,
            Link7 = 214693,
            Link8 = 214692,
            Link9 = 214717,
        }
    }
}
