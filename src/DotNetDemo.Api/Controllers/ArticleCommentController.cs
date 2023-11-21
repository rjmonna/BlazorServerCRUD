using DotNetDemo.Api.Models;
using DotNetDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetDemo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleCommentController : ControllerBase
    {
        private readonly IArticleCommentRepository _articleCommentRepository;

        public ArticleCommentController(IArticleCommentRepository articleCommentRepository)
        {
            _articleCommentRepository = articleCommentRepository;
        }

        [HttpGet("open")]
        public async Task<ActionResult<IEnumerable<Models.Azure.ArticleComment>>> GetPendingArticleComments()
        {
            return Ok(await _articleCommentRepository.GetPending());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Azure.ArticleComment>> GetArticleComment(Guid id)
        {
            return Ok(await _articleCommentRepository.Get(id));
        }

        [HttpPut("{id}/approve")]
        public async Task<ActionResult> ApproveArticleComment(Guid id)
        {
            await _articleCommentRepository.Approve(id);
            
            return Ok();
        }

        [HttpPut("{id}/decline")]
        public async Task<ActionResult> DeclineArticleComment(Guid id)
        {
            await _articleCommentRepository.Decline(id);
            
            return Ok();
        }
    }
}
