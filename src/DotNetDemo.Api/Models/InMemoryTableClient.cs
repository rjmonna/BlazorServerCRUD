using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Azure;
using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;

namespace DotNetDemo.Api.Models
{
    public class InMemoryTableClient : TableClient
    {
        private Dictionary<string, Dictionary<string, ITableEntity>> _tableStore;

        private static Mapper _mapper;

        static InMemoryTableClient()
        {
            var configuration = new MapperConfiguration(cfg => {});

            _mapper = new Mapper(configuration);
        }

        public override async Task<Response<T>> GetEntityAsync<T>(string partitionKey, string rowKey, IEnumerable<string> select = null, CancellationToken cancellationToken = default)
        {
            var record = _tableStore[partitionKey]?[rowKey];

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

            var record = _tableStore[typedEntity.PartitionKey]?[typedEntity.RowKey];

            Mock<Response> responseMock = new();

            if (record == null)
            {
                responseMock.SetupGet(r => r.Status).Returns(409);
            }
            else
            {
                responseMock.SetupGet(r => r.Status).Returns(200);
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
            return base.QueryAsync<T>(filter, maxPerPage, select, cancellationToken);
        }

        public override Task<Response> UpdateEntityAsync<T>(T entity, ETag ifMatch, TableUpdateMode mode = TableUpdateMode.Merge, CancellationToken cancellationToken = default)
        {
            return base.UpdateEntityAsync(entity, ifMatch, mode, cancellationToken);
        }

        public override Task<Response> UpsertEntityAsync<T>(T entity, TableUpdateMode mode = TableUpdateMode.Merge, CancellationToken cancellationToken = default)
        {
            return base.UpsertEntityAsync(entity, mode, cancellationToken);
        }

        private static List<T> MapDynamicList<T>(IEnumerable<object> obj)
        {
            if (obj == null) return null;

            return obj
                .Select(MapDynamic<T>)
                .ToList();
        }

        private static T MapDynamic<T>(object obj)
        {
            return _mapper.Map<T>(obj);
        }
    }
}