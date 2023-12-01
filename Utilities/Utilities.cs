using SpeedyAir.Models; 
using Newtonsoft.Json;  

namespace SpeedyAir.Utilities
{
    public static class Utilities
    { 
        public static void DrawLine(int lenght, string type = "--")
        { 
            for (int i = 0; i < lenght; i++)
                Console.Write(type);
        }

        static void DrawBoxWithText(string text, int width)
        {
            Console.WriteLine();
            DrawLine(width, '+'); 
             
            int padding = (width - text.Length) / 2 - 1;
            padding = Math.Max(padding, 0);  
             
            Console.WriteLine("+" + new string(' ', padding) + text + new string(' ', width - text.Length - padding - 2) + "+");
             
            DrawLine(width, '+'); 
        }

        static void DrawLine(int width, char borderChar)
        {
            Console.WriteLine(new string(borderChar, width));
        }

        public static void LoadMenu()
        { 
            DrawBoxWithText("SpeedyAir App", 40);  
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Show Orders");
            Console.WriteLine("2. Add Flight Details Manually");
            Console.WriteLine("3. Load Flight Details from JSON File");
            Console.WriteLine("4. Generate flight itineraries");
            Console.WriteLine("5. Exit");
            DrawLine(20, "**");
            Console.WriteLine(); 
        }

        public static List<Order> ConvertFileToJsonArray(string filePath)
        {
            var orders = new List<Order>();

            var fileContent = File.ReadAllText(filePath);
            var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(fileContent);

            if (data != null)
            {
                foreach (var item in data)
                    orders.Add(new Order { Code = item.Key, Destination = item.Value["destination"] });
            }

            return orders;
        }
    }
}
