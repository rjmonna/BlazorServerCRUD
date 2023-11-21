namespace DotNetDemo.Api.Models
{
    public class ArticleCommentRepository : IArticleCommentRepository
    {
        private readonly IAzureTableStorage _azureTableStorage;

        public ArticleCommentRepository(IAzureTableStorage azureTableStorage) {
            _azureTableStorage = azureTableStorage;
        }

        public async Task CreateArticleComment(Azure.ArticleComment comment)
        {
            comment.PartitionKey = "ArticleComment";
            comment.RowKey = Guid.NewGuid().ToString();

            await _azureTableStorage.AddAsync("ArticleComment", comment);
        }

        public async Task<Azure.ArticleComment> Get(Guid id)
        {
            return await _azureTableStorage.GetAsync<Azure.ArticleComment>("ArticleComment", "ArticleComment", id.ToString());
        }

        public async Task<IEnumerable<Azure.ArticleComment>> GetPending()
        {
            return await _azureTableStorage.QueryAsync<Azure.ArticleComment>("ArticleComment", (_) => true);
        }

        public async Task Approve(Guid id)
        {
            var entity = await _azureTableStorage.GetAsync<Azure.ArticleComment>("ArticleComment", "ArticleComment", id.ToString());

            entity.IsApproved = true;

            await _azureTableStorage.UpdateAsync("ArticleComment", entity);
        }

        public async Task Decline(Guid id)
        {
            var entity = await _azureTableStorage.GetAsync<Azure.ArticleComment>("ArticleComment", "ArticleComment", id.ToString());

            entity.IsDeclined = true;

            await _azureTableStorage.UpdateAsync("ArticleComment", entity);
        }
    }
}