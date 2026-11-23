using System.Data.SqlClient;
using Newtonsoft.Json;

namespace LegacyOrderService;

/// <summary>
/// Data-access layer for the legacy order processing system.
/// Written years ago, never revisited: string-concatenated SQL and
/// pinned to an outdated serializer. This is intentionally left as-is
/// to demonstrate modernization tooling against realistic legacy code.
/// </summary>
public class OrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    /// <summary>
        /// Looks up orders for a given customer using a parameterized query.
    /// </summary>
    public List<string> GetOrdersForCustomer(string customerId)
    {
        var results = new List<string>();

        using var connection = new SqlConnection(_connectionString);
        connection.Open();

        var query = "SELECT OrderId, Status, Total FROM Orders WHERE CustomerId = @CustomerId";

        using var command = new SqlCommand(query, connection);
        command.Parameters.Add("@CustomerId", System.Data.SqlDbType.NVarChar).Value =
            customerId ?? (object)DBNull.Value;
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var order = new
            {
                OrderId = reader.GetInt32(0),
                Status = reader.GetString(1),
                Total = reader.GetDecimal(2),
            };

            results.Add(JsonConvert.SerializeObject(order));
        }

        return results;
    }
}
