using QuizMaker.Application.DTOs;

namespace QuizMaker.Application.Services
{
    public interface IQuizService
    {
        Task<IEnumerable<BaseQuizDTO>> GetQuizzesPaginatedAsync(int pageNumber, int pageSize);
        Task<QuizDTO?> GetQuizByIdAsync(int id);
        Task<IEnumerable<QuestionDTO>> GetQuestionsAsync(string searchText, int maxRows);
        Task<QuizDTO> CreateQuizAsync(CreateQuizDTO quiz);
        Task<bool> UpdateQuizAsync(int id, UpdateQuizDTO quiz);
        Task<bool> SoftDeleteAsync(int quizId);
    }
}
