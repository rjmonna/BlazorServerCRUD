using DotNetDemo.Models;

namespace DotNetDemo.Web.Services
{
    public class ArticleCommentService : IArticleCommentService
    {
        private readonly HttpClient _httpClient;

        public ArticleCommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CommentArticle(ArticleComment comment)
        {
            await _httpClient.PostAsJsonAsync($"api/articlecomment", comment);
        }

        public async Task<IEnumerable<ArticleComment>> GetPendingArticleComments()
        {
            return await _httpClient.GetFromJsonAsync<ArticleComment[]>("api/articlecomment/open");
        }

        public async Task ApproveComment(Guid id)
        {
            await _httpClient.PostAsJsonAsync<Guid>($"api/articlecomment/approval", id);

            return;
        }
    }
}