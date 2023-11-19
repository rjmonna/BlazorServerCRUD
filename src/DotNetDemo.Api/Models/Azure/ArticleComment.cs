using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Data.Tables;

namespace DotNetDemo.Api.Models.Azure
{
    public class ArticleComment : ITableEntity
    {
        public string PartitionKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string RowKey { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public DateTimeOffset? Timestamp { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ETag ETag { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Guid ArticleCommentId { get; set; }

        public Guid ArticleId { get; set; }
        
        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ModificationDate { get; set; }

        public DateTime? DeletionDate { get; set; }

        public bool IsDismissed { get; set; }
    }
}