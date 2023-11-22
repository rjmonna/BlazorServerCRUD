using System.Diagnostics;
using Azure;
using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Moq;

namespace DotNetDemo.Api.Models
{
    public class InMemoryTableServiceClient : TableServiceClient
    {
        private static Dictionary<string, InMemoryTableClient> s_tableClients = new Dictionary<string, InMemoryTableClient>();

        public override TableClient GetTableClient(string tableName)
        {
            CreateTableIfNotExists(tableName);

            return s_tableClients[tableName];
        }

        public override async Task<Response<TableItem>> CreateTableIfNotExistsAsync(string tableName, CancellationToken cancellationToken = default)
        {
            if (!s_tableClients.ContainsKey(tableName))
            {
                s_tableClients[tableName] = new InMemoryTableClient();
            }

            Mock<Response> responseMock = new();
            responseMock.SetupGet(r => r.Status).Returns(200);

            Response response = responseMock.Object;
            return Response.FromValue(new TableItem(tableName), response);
        }

        public override Response<TableItem> CreateTableIfNotExists(string tableName, CancellationToken cancellationToken = default)
        {
            return CreateTableIfNotExistsAsync(tableName, cancellationToken).Result;
        }
    }
}