using QuizMaker.Application.DTOs;

namespace QuizMaker.Application.Services
{
    public interface IQuizExportService
    {
        IEnumerable<string> GetAvailableExportTypes();
        Task<ExportDataDTO?> GetExportDataByType(QuizDTO quiz, string exportType);
    }
}
