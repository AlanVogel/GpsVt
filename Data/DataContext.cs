using Microsoft.EntityFrameworkCore;
using SpeedSight.Models;

namespace SpeedSight.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options)
        {

        }

        public DbSet<GpsData> GpsDatas { get; set; }
    }
}
