// Legacy Order Service - sample app used to demonstrate modernization
// (security + reliability) with GitHub Copilot.
using LegacyOrderService;

var connectionString = Environment.GetEnvironmentVariable("ORDERS_CONNECTION_STRING")
    ?? "Server=localhost;Database=Orders;Trusted_Connection=True;";

var repository = new OrderRepository(connectionString);

Console.WriteLine("Legacy Order Service starting up...");
Console.WriteLine("Enter a customer id to look up orders:");

var customerId = Console.ReadLine() ?? string.Empty;

try
{
    var orders = repository.GetOrdersForCustomer(customerId);
    foreach (var order in orders)
    {
        Console.WriteLine(order);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Failed to load orders: {ex.Message}");
}
