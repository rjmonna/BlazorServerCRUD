using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DotNetDemo.Api.Models
{
    public class AzureTableRepository
    {
        TableClient _tableClient;

        public AzureTableRepository(TableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public async Task<T> GetAsync<T>(string partitionKey, string rowKey, CancellationToken cancellationToken = default) where T : class, ITableEntity {
            await _tableClient.CreateIfNotExistsAsync();

            var result = await _tableClient.GetEntityAsync<T>(partitionKey, rowKey, null, cancellationToken);

            return result.Value;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, ITableEntity {
            await _tableClient.CreateIfNotExistsAsync();

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> QueryAsync<T>() where T : class, ITableEntity {
            await _tableClient.CreateIfNotExistsAsync();

            throw new NotImplementedException();
        }

        public async Task<object> AddOrUpdateAsync(ITableEntity entity) {
            await _tableClient.CreateIfNotExistsAsync();

            throw new NotImplementedException();
        }

        public async Task<object> DeleteAsync(ITableEntity entity) {
            await _tableClient.CreateIfNotExistsAsync();

            throw new NotImplementedException();
        }

        public async Task<object> AddAsync(ITableEntity entity) {
            await _tableClient.CreateIfNotExistsAsync();

            _tableClient.AddEntity(entity);

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> AddBatchAsync<T>() where T : class, ITableEntity {
            await _tableClient.CreateIfNotExistsAsync();

            throw new NotImplementedException();
        }

        public async Task<object> UpdateAsync() {
            await _tableClient.CreateIfNotExistsAsync();

            throw new NotImplementedException();
        }
    }
}