using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetDemo.Models;

namespace DotNetDemo.Web.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<Article>> SearchArticles();

        Task<Article> GetArticle(Guid id);

        Task AddArticle(Article article);

        Task UpdateArticle(Article article);

        Task DeleteArticle(Guid id);

        Task CommentArticle(ArticleComment comment);
    }
}