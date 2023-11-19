using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DotNetDemo.Api.Models
{
    public class InMemoryTableServiceClient : TableServiceClient
    {
        Dictionary<string, TableClient> _tableClients;

        public InMemoryTableServiceClient(Dictionary<string, TableClient> tableClients) : base()
        {
            _tableClients = tableClients;
        }

        public override TableClient GetTableClient(string tableName)
        {
            return _tableClients[tableName] ?? base.GetTableClient(tableName);
        }
    }
}