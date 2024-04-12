using SpeedSight.Data;
using SpeedSight.Interfaces;
using SpeedSight.Models;
using SpeedSight.Repository.helper;

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
            return _context.GpsDatas.Where(d => d.Id <= 10).OrderBy(p => p.Id).ToList();
        }

        public double GetAvgSpeedForId(int id)
        {
            var data_id = _context.GpsDatas.Where(p => p.Id == id).FirstOrDefault();

            if(data_id == null)
                return 0;

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
            if (Utils.IsInRangeLink(data.Link))
            {
                Utils.SetUtc(data);
                _context.Add(data);
                return Save();
            }
            return false;
        }

        public bool UpdateGpsData(GpsData data)
        {
            if (Utils.IsInRangeLink(data.Link))
            {
                Utils.SetUtc(data);
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

        public Dictionary<int,double> GetAvgSpeedForAll()
        {
            Dictionary<int, Tuple<double, int>> speedByLinks = new Dictionary<int, Tuple<double, int>>();
            var dataLinks = _context.GpsDatas.OrderBy(p => p.Id).ToList();

            foreach (var dataLink in dataLinks) 
            {
                if (speedByLinks.ContainsKey(dataLink.Link))
                {
                    var tuple = speedByLinks[dataLink.Link];
                    double sum = tuple.Item1 + dataLink.Match_Speed;
                    int count = tuple.Item2 + 1;
                    speedByLinks[dataLink.Link] = Tuple.Create(sum, count);
                }
                else
                    speedByLinks.Add(dataLink.Link, new Tuple<double,int>(dataLink.Match_Speed, 1));
            }

            if (speedByLinks.Count <= 0)
                return new Dictionary<int, double> {};

            Dictionary<int, double> avgSpeed = new Dictionary<int, double>();

            foreach (var kv in speedByLinks)
            {
                double avg_speed = Math.Round(kv.Value.Item1 / kv.Value.Item2, 2);
                avgSpeed.Add(kv.Key, avg_speed);
            }

            return avgSpeed;
        }
    }
}
