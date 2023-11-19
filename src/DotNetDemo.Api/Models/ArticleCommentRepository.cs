using System.Runtime.InteropServices;
using Azure.Data.Tables;
using DotNetDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

namespace DotNetDemo.Api.Models
{
    public class ArticleCommentRepository : IArticleCommentRepository
    {
        private readonly AzureTableStorage _azureTableStorage;
        public ArticleCommentRepository(TableServiceClient tableServiceClient) {
            _azureTableStorage = new AzureTableStorage(tableServiceClient);
        }

        public async Task CreateArticleComment(Azure.ArticleComment comment)
        {
            comment.PartitionKey = "ArticleComment";
            comment.RowKey = Guid.NewGuid().ToString();

            await _azureTableStorage.AddAsync("ArticleComment", comment);
        }
    }
}