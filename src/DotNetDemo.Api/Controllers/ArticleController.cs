using DotNetDemo.Api.Models;
using DotNetDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;

        public ArticleController(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<ActionResult> SearchArticles()
        {
            return Ok(await _articleRepository.SearchArticles());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetArticle(Guid id)
        {
            return Ok(await _articleRepository.GetArticle(id));
        }

        [HttpPost]
        public async Task<ActionResult> CreateArticle(Article article)
        {
            await _articleRepository.CreateArticle(article);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateArticle(Article article)
        {
            await _articleRepository.UpdateArticle(article);
            
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult DeleteArticle(Guid id)
        {
            _articleRepository.DeleteArticle(id);

            return Ok();
        }
    }
}
