namespace DotNetDemo.Api.Models
{
    public interface IArticleCommentRepository
    {

        Task CreateArticleComment(Azure.ArticleComment articleComment);

        Task<Azure.ArticleComment> Get(Guid id);

        Task<IEnumerable<Azure.ArticleComment>> GetPending();

        Task Approve(Guid id);

        Task Decline(Guid id);
    }
}