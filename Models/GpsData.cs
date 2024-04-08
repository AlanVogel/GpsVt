namespace SpeedSight.Models
{
    public class GpsData
    {
        public int Id { get; set; }
        public int Link { get; set; }
        public int Utc { get; set; }
        public double Match_X { get; set; }
        public double Match_Y { get; set;}
        public double Org_X { get; set;}
        public double Org_Y { get; set;}
        public int Match_Distance { get; set; }
        public int Match_H { get; set; }
        public int Match_Speed { get; set; }
        public int Org_H { get; set; }
        public int Org_Speed { get; set; }
    }
}
