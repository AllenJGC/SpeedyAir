using SpeedyAir.Models; 

namespace SpeedyAir.Services
{
    using Newtonsoft.Json;
    using SpeedyAir.Utilities;
    public class FlightService
    {
        public static void CreateFlightItineraries(string orderPath, List<FlightDetails> flights)
        {
            var orders = Utilities.ConvertFileToJsonArray(orderPath);

            Console.WriteLine();
            Console.WriteLine("> Flight itineraries:");
            Console.WriteLine();
            if (flights.Count == 0)
                Console.WriteLine("!!! Flight should be loaded first. !!!");

            var dicFlight = new Dictionary<string, int>();
            var assignedOrders = new List<Order>();
            foreach (var flight in flights)
            {
                if (!dicFlight.TryGetValue(flight.ArrivalCode, out var skip))
                    skip = 0;

                var flightTotal = orders.Where(x => x.Destination == flight.ArrivalCode)
                                        .Skip(skip)
                                        .Take(flight.Capacity)
                                        .ToList();

                assignedOrders.AddRange(flightTotal);

                if (flightTotal.Any())
                {
                    var detail = new OrdersFlights
                    {
                        Day = flight.Day,
                        Flight = flight.Flight,
                        Arrival = flight.Arrival,
                        ArrivalCode = flight.ArrivalCode,
                        TotalBoxes = flightTotal.Count
                    };

                    Console.WriteLine();

                    Console.WriteLine($"> Day: {detail.Day} | flight: {detail.Flight} | " +
                    $" Arrival Code: {detail.ArrivalCode} | Arrival: {detail.Arrival}");

                    Console.WriteLine();

                    var chunkSize = 6;
                    for (int i = 0; i < flightTotal.Count; i += chunkSize)
                    {
                        var chunk = flightTotal.Skip(i).Take(chunkSize).Select(x => x.Code);
                        Console.WriteLine(String.Join(" | ", chunk));
                    }

                    Console.WriteLine();

                    Console.WriteLine($"> Total Boxes: {detail.TotalBoxes.ToString().PadLeft(detail.TotalBoxes > 10 ? 0 : 2)}");

                    Utilities.DrawLine(35);

                    Console.WriteLine();

                    dicFlight[flight.ArrivalCode] = skip + flightTotal.Count;
                }
            }

            var unassignedOrders = orders.Except(assignedOrders).ToList();

            Console.WriteLine();
            Console.WriteLine("!!! Unassigned Orders !!!");
            foreach (var order in unassignedOrders)
                Console.WriteLine($" Order Code: {order.Code}, Destination: {order.Destination}");
        }

        public static void PrintFlights(List<FlightDetails> flights)
        {
            Console.WriteLine("Flights available:");
            foreach (var flight in flights)
            {
                Console.WriteLine($"Day: {flight.Day} | flight: {flight.Flight} |" +
                           $" Arrival Code: {flight.ArrivalCode} | Arrival: {flight.Arrival}");
            }
        }

        public static List<FlightDetails> AddFlightDetails()
        {
            var flights = new List<FlightDetails>();
            bool addAnother = true;
            while (addAnother)
            {
                var flight = new FlightDetails();

                try
                {
                    Console.WriteLine("Enter the day of the flight (number):");
                    flight.Day = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input was not a number. Please enter a valid number.");
                    continue; // Restart the loop if an error occurs
                }

                Console.WriteLine("Enter the flight number:");
                flight.Flight = Console.ReadLine();

                Console.WriteLine("Enter the departure city:");
                flight.Departure = Console.ReadLine();

                Console.WriteLine("Enter the departure airport code:");
                flight.DepartureCode = Console.ReadLine();

                Console.WriteLine("Enter the arrival city:");
                flight.Arrival = Console.ReadLine();

                Console.WriteLine("Enter the arrival airport code:");
                flight.ArrivalCode = Console.ReadLine();

                flights.Add(flight);
                Console.WriteLine("Flight added successfully!\n");

                Console.WriteLine("Do you want to add another flight? (y/n)");
                string response = Console.ReadLine().ToLower();
                addAnother = response == "y";
            }

            return flights;
        }

        public static List<FlightDetails> LoadFlightsFromJson(string currentDisk, string currentDomain, string filePath)
        {
            try
            {
                var flightPath = Path.Combine(currentDisk, currentDomain, filePath);
                var flightJson = File.ReadAllText(flightPath);
                Console.WriteLine("Flights loaded successfully from JSON file.\n");

                return JsonConvert.DeserializeObject<List<FlightDetails>>(flightJson);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading flights from JSON: {ex.Message}");
                return null;
            }
        }
    }
}
