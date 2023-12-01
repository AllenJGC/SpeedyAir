namespace SpeedyAir.Models
{
    public class FlightDetails
    {
        public int Day { get; set; }
        public string Flight { get; set; }
        public string Departure { get; set; }
        public string DepartureCode { get; set; }
        public string Arrival { get; set; } 
        public string ArrivalCode { get; set; } 
        public int Capacity { get; } = 20;
    }
}
