namespace SpeedyAir.Services
{ 
    using SpeedyAir.Utilities; 
    public static class OrderService
    {
        public static void ShowOrders(string orderPath)
        {
            var orders = Utilities.ConvertFileToJsonArray(orderPath);

            Utilities.DrawLine(15);
            Console.WriteLine();
            Console.WriteLine("Orders:"); 
            var groupedOrders = orders
                        .GroupBy(order => order.Destination)
                        .Select(group => new { Destination = group.Key, Count = group.Count() });
            Console.WriteLine();

            foreach (var group in groupedOrders)
                Console.WriteLine($"Destination: {group.Destination}, Count: {group.Count}");

            Console.WriteLine();
            Console.WriteLine($"Total: {orders.Count()}");
            Console.WriteLine();
            foreach (var order in orders)
                Console.WriteLine($"{order.Code} => Destination: {order.Destination}"); 
        } 
    }
}
