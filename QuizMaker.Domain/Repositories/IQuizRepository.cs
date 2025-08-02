using QuizMaker.Domain.Entities;

namespace QuizMaker.Domain.Repositories
{
    public interface IQuizRepository
    {
        Task<IEnumerable<Quiz>> GetQuizzesPaginatedAsync(int pageNumber, int pageSize);
        Task<Quiz?> GetQuizByIdAsync(int id);
        Task<IEnumerable<Question>> GetQuestionsAsync(string searchText, int maxRows);
        Task<Quiz> CreateQuizAsync(Quiz quiz);
        Task<bool> UpdateQuizAsync(int id, Quiz quiz);
        Task<bool> SoftDeleteAsync(int quizId);
    }
}
