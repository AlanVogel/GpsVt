using SpeedSight.Data;
using SpeedSight.Models;

namespace SpeedSight
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        public Seed(DataContext context)
        {
            this._dataContext = context;
        }

        public DataContext GetDataContext() 
        {
            return _dataContext;
        }

        public void SeedDataContext()
        {
            if (!_dataContext.GpsDatas.Any())
            {
                var datacontext = GetDataContext();
                new ReadData(new List<GpsData>(), datacontext).ReadGpsData();
            }   
        }
    }
}
