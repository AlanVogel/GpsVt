using SpeedSight.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpeedSight.Repository.helper
{
    public static class Utils
    {
        public static GpsData SetUtc(GpsData data) 
        {
            TimeSpan span = DateTime.Now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            data.Utc = (int)(span.TotalSeconds);
            return data;
        }

        public static bool IsInRangeLink(int link)
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

            Link10 = -201678,
            Link11 = -201679,
            Link12 = -214697,
            Link13 = -214696,
            Link14 = -214695,
            Link15 = -214694,
            Link16 = -214693,
            Link17 = -214692,
            Link18 = -214717
        }
    }
}
