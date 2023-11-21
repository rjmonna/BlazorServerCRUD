using System.Linq.Expressions;
using System.Reflection;
using AutoMapper;
using Azure;
using Azure.Data.Tables;
using Moq;
using StringToExpression.LanguageDefinitions;

namespace DotNetDemo.Api.Models
{
    public class InMemoryTableClient : TableClient
    {
        private Dictionary<string, Dictionary<string, DynamicTableEntity>> _tableStore = new Dictionary<string, Dictionary<string, DynamicTableEntity>>();

        private static IMapper _mapper;

        static InMemoryTableClient()
        {
            var configuration = new MapperConfiguration(cfg => {});

            _mapper = configuration.CreateMapper();
        }

        public override async Task<Response<T>> GetEntityAsync<T>(string partitionKey, string rowKey, IEnumerable<string> select = null, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrWhiteSpace(partitionKey)) throw new ArgumentOutOfRangeException(nameof(partitionKey));
            if (String.IsNullOrWhiteSpace(rowKey)) throw new ArgumentOutOfRangeException(nameof(rowKey));

            var record = _tableStore.ContainsKey(partitionKey) && _tableStore[partitionKey].ContainsKey(rowKey) ? _tableStore[partitionKey][rowKey] : null;

            Mock<Response> responseMock = new();

            if (record == null)
            {
                responseMock.SetupGet(r => r.Status).Returns(404);
            }
            else
            {
                responseMock.SetupGet(r => r.Status).Returns(200);
            }

            Response response = responseMock.Object;
            return Response.FromValue<T>(MapDynamic<T>(record), response);
        }

        public override async Task<Response> AddEntityAsync<T>(T entity, CancellationToken cancellationToken = default)
        {
            var typedEntity = (ITableEntity)entity;

            if (String.IsNullOrWhiteSpace(entity.PartitionKey) || String.IsNullOrWhiteSpace(entity.RowKey)) throw new InvalidOperationException("ITableEntity must hava a PartitionKey and an RowKey.");

            var record = _tableStore.ContainsKey(typedEntity.PartitionKey) && _tableStore[typedEntity.PartitionKey].ContainsKey(typedEntity.RowKey) ? _tableStore[typedEntity.PartitionKey][typedEntity.RowKey] : null;

            Mock<Response> responseMock = new();

            if (record == null)
            {
                if (!_tableStore.ContainsKey(typedEntity.PartitionKey))
                {
                    _tableStore.Add(typedEntity.PartitionKey, new Dictionary<string, DynamicTableEntity>());
                }

                _tableStore[typedEntity.PartitionKey][typedEntity.RowKey] = MapDynamic(entity);

                responseMock.SetupGet(r => r.Status).Returns(200);
            }
            else
            {
                responseMock.SetupGet(r => r.Status).Returns(409);
            }

            Response response = responseMock.Object;
            return response;
        }

        public override AsyncPageable<T> QueryAsync<T>(Expression<Func<T, bool>> filter, int? maxPerPage = null, IEnumerable<string> select = null, CancellationToken cancellationToken = default)
        {
            var result = _tableStore
                .Values
                .SelectMany(d => MapDynamicList<T>(d.Values));

            Mock<Response> responseMock = new();

            responseMock.SetupGet(r => r.Status).Returns(200);

            Response response = responseMock.Object;

            List<Page<T>> pages = new List<Page<T>>();

            int maxPageSize = maxPerPage ?? int.MaxValue;

            for(int i = 0; i < result.Count(); i += maxPageSize)
            {
                pages
                    .Add(Page<T>.FromValues(result.Skip(i).Take(maxPageSize).ToList().AsReadOnly(), null, response));
            }

            return AsyncPageable<T>.FromPages(pages);
        }

        public override AsyncPageable<T> QueryAsync<T>(string filter = null, int? maxPerPage = null, IEnumerable<string> select = null, CancellationToken cancellationToken = default)
        {
            var language = new ODataFilterLanguage();

            Expression<Func<T, bool>> predicate = language.Parse<T>(filter);

            return QueryAsync<T>(predicate, maxPerPage, select, cancellationToken);
        }

        public override async Task<Response> UpdateEntityAsync<T>(T entity, ETag ifMatch, TableUpdateMode mode = TableUpdateMode.Merge, CancellationToken cancellationToken = default)
        {
            var typedEntity = (ITableEntity)entity;

            if (String.IsNullOrWhiteSpace(entity.PartitionKey) || String.IsNullOrWhiteSpace(entity.RowKey)) throw new InvalidOperationException("ITableEntity must hava a PartitionKey and an RowKey.");

            var record = _tableStore.ContainsKey(typedEntity.PartitionKey) && _tableStore[typedEntity.PartitionKey].ContainsKey(typedEntity.RowKey) ? _tableStore[typedEntity.PartitionKey][typedEntity.RowKey] : null;

            Mock<Response> responseMock = new();

            if (record == null)
            {
                responseMock.SetupGet(r => r.Status).Returns(404);
            }
            else
            {
                _tableStore[typedEntity.PartitionKey][typedEntity.RowKey] = MapDynamic(entity);

                responseMock.SetupGet(r => r.Status).Returns(200);
            }

            Response response = responseMock.Object;
            return response;
        }

        public override async Task<Response> UpsertEntityAsync<T>(T entity, TableUpdateMode mode = TableUpdateMode.Merge, CancellationToken cancellationToken = default)
        {
            var typedEntity = (ITableEntity)entity;

            if (String.IsNullOrWhiteSpace(entity.PartitionKey) || String.IsNullOrWhiteSpace(entity.RowKey)) throw new InvalidOperationException("ITableEntity must hava a PartitionKey and an RowKey.");

            Mock<Response> responseMock = new();

            _tableStore[typedEntity.PartitionKey][typedEntity.RowKey] = MapDynamic(entity);

            responseMock.SetupGet(r => r.Status).Returns(200);

            Response response = responseMock.Object;
            return response;
        }

        private static List<T> MapDynamicList<T>(IEnumerable<DynamicTableEntity> obj)
        {
            if (obj == null) return null;

            return obj
                .Select(MapDynamic<T>)
                .ToList();
        }

        private static T MapDynamic<T>(DynamicTableEntity obj)
        {
            return _mapper.Map<T>(obj);
        }

        private static List<DynamicTableEntity> MapDynamicList<T>(IEnumerable<T> obj)
        {
            if (obj == null) return null;

            return obj
                .Select(MapDynamic<T>)
                .ToList();
        }

        private static DynamicTableEntity MapDynamic<T>(T obj)
        {
            return _mapper
                .Map<DynamicTableEntity>(typeof(T)
                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                    .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null)));
        }
    }
}