using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetDemo.Models;

namespace DotNetDemo.Web.Services
{
    public class ArticleService : IArticleService
    {
        private readonly HttpClient _httpClient;

        public ArticleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task AddArticle(Article article)
        {
            await _httpClient.PostAsJsonAsync($"api/article", article);
        }

        public async Task UpdateArticle(Article article)
        {
            await _httpClient.PutAsJsonAsync($"api/article", article);
        }

        public async Task DeleteArticle(Guid id)
        {
            await _httpClient.DeleteAsync($"api/article/{id}");
        }

        public async Task<Article> GetArticle(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Article>($"api/article/{id}");
        }

        public async Task<IEnumerable<Article>> SearchArticles()
        {
            return await _httpClient.GetFromJsonAsync<Article[]>("api/article?top=10");
        }
    }
}