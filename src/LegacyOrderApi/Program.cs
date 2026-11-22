using LegacyOrderApi;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var connectionString = builder.Configuration["ORDERS_CONNECTION_STRING"]
    ?? "Server=localhost;Database=Orders;Trusted_Connection=True;";

// Legacy endpoint: customerId comes straight from the route and is
// passed, unvalidated, into the data-access layer.
app.MapGet("/orders/{customerId}", (string customerId) =>
{
    var repository = new OrderRepository(connectionString);
    var orders = repository.GetOrdersForCustomer(customerId);
    return Results.Ok(orders);
});

app.Run();
