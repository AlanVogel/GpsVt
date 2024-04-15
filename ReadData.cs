using SpeedSight.Data;
using SpeedSight.Models;

namespace SpeedSight
{
    public class ReadData
    {
        private readonly DataContext _dataContext;

        public ReadData(DataContext context)
        {
            this._dataContext = context;
        }

        public async Task ReadGpsDataAsync(string data_path = @"\\gps_data.txt",
                                string new_line = "NEW_ROUTE")
        {
            try
            {
                string dir = new DirectoryInfo(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
                string path = Path.GetFullPath(dir + data_path);
                var NumberOfProperties = typeof(GpsData).GetProperties().Length;
                var batchSize = 1000;

                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    var records = new List<GpsData>();

                    while ((line = await sr.ReadLineAsync()) != null)
                    {
                        if (line != new_line)
                        {
                            string[] parts = line.Split(';');
                            if (parts.Length == NumberOfProperties - 1)
                            {
                                var record = new GpsData
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
                                };
                                records.Add(record);

                                if (records.Count >= batchSize)
                                {
                                    _dataContext.GpsDatas.AddRange(records);
                                    await _dataContext.SaveChangesAsync();
                                    records.Clear();
                                }
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
