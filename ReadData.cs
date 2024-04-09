using SpeedSight.Data;
using SpeedSight.Models;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpeedSight
{
    public class ReadData
    {
        private readonly List<GpsData> _data;
        private readonly DataContext _dataContext;

        public ReadData(List<GpsData> data, DataContext context)
        {
            this._data = data;
            this._dataContext = context;
        }

        public void ReadGpsData(string path = @"C:\Users\AlanV\Desktop\Leo-podaci\gps_data_test.txt",
                                string new_line = "NEW_ROUTE")
        {
            try
            {
                var NumberOfProperties = typeof(GpsData).GetProperties().Length;

                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line != new_line)
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length == NumberOfProperties - 1)
                            {
                                var _data = new List<GpsData>()
                                {
                                    new GpsData
                                    {
                                        Link = int.Parse(parts[0]),
                                        Utc = int.Parse(parts[1]),
                                        Match_X = double.Parse(parts[2], System.Globalization.CultureInfo.InvariantCulture),
                                        Match_Y = double.Parse(parts[3], System.Globalization.CultureInfo.InvariantCulture),
                                        Org_X = double.Parse(parts[4], System.Globalization.CultureInfo.InvariantCulture),
                                        Org_Y = double.Parse(parts[5], System.Globalization.CultureInfo.InvariantCulture),
                                        Match_Distance = int.Parse(parts[6]),
                                        Match_H = int.Parse(parts[7]),
                                        Match_Speed = int.Parse(parts[8]),
                                        Org_H = int.Parse(parts[9]),
                                        Org_Speed = int.Parse(parts[10])
                                    }
                                };
                                _dataContext.GpsDatas.AddRange(_data);
                                _dataContext.SaveChanges();
                            }
                            else
                                throw new Exception("The number of records are missing.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Invalid data format: ", ex);
            }
        }
    }
}
