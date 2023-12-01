using SpeedyAir.Models;
using SpeedyAir.Services;
using SpeedyAir.Utilities; 

//var orders = Utilities.LoadOrdersFromJson("../../../Data/coding-assigment-orders.json");
var currentDisk = Path.GetPathRoot(Environment.CurrentDirectory);
var currentDomain = AppDomain.CurrentDomain.FriendlyName;
const string ORDER_FILE_PATH = @"Data\coding-assigment-orders.json";
const string FLIGHTS_FILE_PATH = @"Data\flights.json";
var orderPath = Path.Combine(currentDisk, currentDomain, ORDER_FILE_PATH);
var flights = new List<FlightDetails>();
var exit = false;

Console.BackgroundColor = ConsoleColor.White;
Console.Clear();
Console.ForegroundColor = ConsoleColor.Black; 

while (!exit)
{
    Utilities.LoadMenu();
    var choice = Console.ReadLine();
    switch (choice)
    {
        case "1":
            Console.Clear();
            OrderService.ShowOrders(orderPath);
            break;
        case "2":
            Console.Clear();
            flights.Clear();
            flights = FlightService.AddFlightDetails();
            FlightService.PrintFlights(flights);
            break;
        case "3":
            Console.Clear();
            flights.Clear();
            flights = FlightService.LoadFlightsFromJson(currentDisk, currentDomain, FLIGHTS_FILE_PATH);
            FlightService.PrintFlights(flights);
            break;
        case "4":
            Console.Clear();
            FlightService.CreateFlightItineraries(orderPath, flights); 
            break;
        case "5":
            exit = true;
            break;
        default:
            Console.WriteLine("Invalid option, please try again.");
            break;
    }
}  
