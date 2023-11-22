using DotNetDemo.Models;

namespace DotNetDemo.Services.Contracts
{
    public interface IArticleCommentService
    {

        Task CommentArticle(ArticleComment comment);

        Task<IEnumerable<ArticleComment>> GetPendingArticleComments();

        Task ApproveComment(Guid id);
    }
}