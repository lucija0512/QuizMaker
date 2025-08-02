using Microsoft.AspNetCore.Mvc;
using QuizMaker.Application.Services;

namespace QuizMaker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizExportController : ControllerBase
    {
        private readonly IQuizExportService _exportService;
        private readonly IQuizService _quizService;

        public QuizExportController(IQuizService quizService, IQuizExportService exportService)
        {
            _quizService = quizService;
            _exportService = exportService;
        }

        /// <summary>
        /// Gets available export types
        /// </summary>
        [HttpGet("ExportTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<string>>> GetAvailableExportTypes()
        {
            return Ok(_exportService.GetAvailableExportTypes());
        }

        /// <summary>
        /// Exports a quiz to a file of the specified type
        /// </summary>
        [HttpGet("{quizId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DownloadQuizFileAsync(int quizId, string exportType)
        {
            var quiz = await _quizService.GetQuizByIdAsync(quizId);
            if (quiz == null)
            {
                return NotFound("Quiz not found.");
            }
            var exportData = await _exportService.GetExportDataByType(quiz, exportType);
            if (exportData == null)
            {
                return BadRequest($"Export type '{exportType}' not supported.");
            }
            return File(exportData.Data, exportType, exportData.FileName);
        }
    }
}
