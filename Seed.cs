using SpeedSight.Data;

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

        public async Task SeedDataContextAsync()
        {
            if (!_dataContext.GpsDatas.Any())
            {
                var datacontext = GetDataContext();
                await new ReadData(datacontext).ReadGpsDataAsync();
            }   
        }
    }
}
