using System.Data.SqlClient;
using Newtonsoft.Json;

namespace LegacyOrderApi;

/// <summary>
/// Data-access layer for the legacy order processing API.
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
    /// Looks up orders for a given customer. Builds the query by
    /// concatenating raw, caller-controlled input directly into the SQL
    /// string (reached from the /orders/{customerId} HTTP route).
    /// </summary>
    public List<string> GetOrdersForCustomer(string customerId)
    {
        var results = new List<string>();

        var connectionStringBuilder = new SqlConnectionStringBuilder(_connectionString)
        {
            Encrypt = true,
        };
        using var connection = new SqlConnection(connectionStringBuilder.ConnectionString);
        connection.Open();

        // Vulnerable: user-controlled input concatenated directly into SQL.
        var query = "SELECT OrderId, Status, Total FROM Orders WHERE CustomerId = '" + customerId + "'";

        using var command = new SqlCommand(query, connection);
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
