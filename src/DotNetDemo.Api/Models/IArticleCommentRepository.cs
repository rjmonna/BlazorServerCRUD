using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetDemo.Models;

namespace DotNetDemo.Api.Models
{
    public interface IArticleCommentRepository
    {

        Task CreateArticleComment(Azure.ArticleComment articleComment);
    }
}