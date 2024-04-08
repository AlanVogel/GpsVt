namespace SpeedSight.Dto
{
    //This is used like a data limitation (data sensitive) to not expose all data from the model
    public class GpsDataDto
    {
        public int Id { get; set; }
        public int Link { get; set; }
        public int Match_Distance { get; set; }
        public int Match_Speed { get; set; }
    }
}
