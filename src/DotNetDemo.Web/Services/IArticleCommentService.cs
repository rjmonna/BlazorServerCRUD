using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetDemo.Models;

namespace DotNetDemo.Web.Services
{
    public interface IArticleCommentService
    {

        Task CommentArticle(ArticleComment comment);

        Task<IEnumerable<ArticleComment>> GetPendingArticleComments();

        Task ApproveComment(Guid id);
    }
}