using System.Linq.Expressions;
using Azure.Data.Tables;

namespace DotNetDemo.Infrastructure.Contracts
{
    public interface IAzureTableStorage
    {

        Task<T> GetAsync<T>(string tableName, string partitionKey, string rowKey, CancellationToken cancellationToken = default) where T : class, ITableEntity;

        Task<IEnumerable<T>> GetAllAsync<T>(string tableName, CancellationToken cancellationToken = default) where T : class, ITableEntity;

        Task<IEnumerable<T>> QueryAsync<T>(string tableName, Expression<Func<T, bool>> query, CancellationToken cancellationToken = default) where T : class, ITableEntity;

        Task<IEnumerable<T>> QueryAsync<T>(string tableName, string? query = null, CancellationToken cancellationToken = default) where T : class, ITableEntity;

        Task<object> AddOrUpdateAsync(string tableName, ITableEntity entity, CancellationToken cancellationToken = default);

        Task<object> DeleteAsync(string tableName, ITableEntity entity, CancellationToken cancellationToken = default);

        Task<object> AddAsync(string tableName, ITableEntity entity);

        Task<IEnumerable<T>> AddBatchAsync<T>(string tableName) where T : class, ITableEntity;

        Task<object> UpdateAsync(string tableName, ITableEntity entity, CancellationToken cancellationToken = default);   }
}