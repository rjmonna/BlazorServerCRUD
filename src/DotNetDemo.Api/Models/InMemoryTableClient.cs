using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace DotNetDemo.Api.Models
{
    public class InMemoryTableClient : TableClient
    {
        private Dictionary<string, Dictionary<string, Dictionary<string, ITableEntity>>> tableStore;

        public override async Task<Response<T>> GetEntityAsync<T>(string partitionKey, string rowKey, IEnumerable<string> select = null, CancellationToken cancellationToken = default(CancellationToken)) where T : class
        {
            throw new NotImplementedException();
        }
    }
}