using DotNetDemo.Models;

namespace DotNetDemo.Api.Models
{
    public interface IArticleRepository
    {
        Task<IEnumerable<Article>> SearchArticles();

        Task<Article?> GetArticle(Guid articleId);

        Task<Article?> CreateArticle(Article article);

        Task<Article?> UpdateArticle(Article article);

        Task<Article?> DeleteArticle(Guid articleId);
    }
}