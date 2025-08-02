using QuizMaker.Application.DTOs;
using QuizMaker.Application.Mappers;
using QuizMaker.Domain.Repositories;

namespace QuizMaker.Application.Services
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _quizRepository;

        public QuizService(IQuizRepository quizRepository)
        {
            _quizRepository = quizRepository;
        }

        public async Task<IEnumerable<BaseQuizDTO>> GetQuizzesPaginatedAsync(int pageNumber, int pageSize)
        {
            var quizzes = await _quizRepository.GetQuizzesPaginatedAsync(pageNumber, pageSize);
            return quizzes.Select(e => e.MapEntityToBaseQuizDto());
        }

        public async Task<QuizDTO?> GetQuizByIdAsync(int id)
        {
            var quiz = await _quizRepository.GetQuizByIdAsync(id);
            return quiz?.MapEntityToQuizDto();
        }

        public async Task<IEnumerable<QuestionDTO>> GetQuestionsAsync(string searchText, int maxRows)
        {
            var questions = await _quizRepository.GetQuestionsAsync(searchText, maxRows);
            return questions.Select(e => e.MapEntityToQuestionDto());
        }

        public async Task<QuizDTO> CreateQuizAsync(CreateQuizDTO quiz)
        {
            var createdQuiz = await _quizRepository.CreateQuizAsync(quiz.MapCreateDtoToEntity());
            return createdQuiz.MapEntityToQuizDto();
        }

        public async Task<bool> UpdateQuizAsync(int id, UpdateQuizDTO quiz)
        {
            return await _quizRepository.UpdateQuizAsync(id, quiz.MapUpdateDtoToEntity());
        }

        public async Task<bool> SoftDeleteAsync(int quizId)
        {
            return await _quizRepository.SoftDeleteAsync(quizId);
        }
    }
}
