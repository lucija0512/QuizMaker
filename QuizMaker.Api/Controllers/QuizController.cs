using Microsoft.AspNetCore.Mvc;
using QuizMaker.Application.DTOs;
using QuizMaker.Application.Services;

namespace QuizMaker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        /// <summary>
        /// Gets a paginated list of quizzes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BaseQuizDTO>>> GetQuizzesAsync(int pageNumber = 1, int pageSize = 20)
        {
            return Ok(await _quizService.GetQuizzesPaginatedAsync(pageNumber, pageSize));
        }

        /// <summary>
        /// Gets quiz by identifier
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<QuizDTO>> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizService.GetQuizByIdAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return Ok(quiz);
        }

        /// <summary>
        /// Gets questions by search text
        /// </summary>
        [HttpGet("Questions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BaseQuizDTO>>> GetQuestionsAsync(string searchText = "", int maxRows = 20)
        {
            return Ok(await _quizService.GetQuestionsAsync(searchText, maxRows));
        }

        /// <summary>
        /// Updates an existing quiz
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateQuizAsync(int id, UpdateQuizDTO quiz)
        {
            var success = await _quizService.UpdateQuizAsync(id, quiz);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Creates a new quiz
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<QuizDTO>> CreateQuizAsync(CreateQuizDTO quiz)
        {
            var createdQuiz = await _quizService.CreateQuizAsync(quiz);
            return CreatedAtAction(nameof(GetQuizByIdAsync), new { id = createdQuiz.Id }, createdQuiz);
        }

        /// <summary>
        /// Soft deletes a quiz
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteQuizAsync(int id)
        {
            var success = await _quizService.SoftDeleteAsync(id);
            if (!success)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
