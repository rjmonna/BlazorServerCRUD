using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Moq;

namespace DotNetDemo.Api.Models
{
    public class InMemoryTableServiceClient : TableServiceClient
    {
        public Dictionary<string, InMemoryTableClient> TableClients { get; set; } = new Dictionary<string, InMemoryTableClient>();

        public override TableClient GetTableClient(string tableName)
        {
            return TableClients[tableName] ?? throw new InvalidOperationException($"Unable to get in memory table client '{tableName}'.");
        }

        public override async Task<Response<TableItem>> CreateTableIfNotExistsAsync(string tableName, CancellationToken cancellationToken = default)
        {
            if (!TableClients.ContainsKey(tableName))
            {
                TableClients[tableName] = new InMemoryTableClient();
            }

            Mock<Response> responseMock = new();
            responseMock.SetupGet(r => r.Status).Returns(200);

            Response response = responseMock.Object;
            return Response.FromValue(new TableItem(tableName), response);
        }
    }
}