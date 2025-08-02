using Microsoft.EntityFrameworkCore;
using QuizMaker.Domain.Entities;
using QuizMaker.Domain.Repositories;
using QuizMaker.Infrastructure.EF;

namespace QuizMaker.Infrastructure.Repositories
{
    public class QuizRepository : IQuizRepository
    {
        private readonly QuizMakerDbContext _dbContext;

        public QuizRepository(QuizMakerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Quiz>> GetQuizzesPaginatedAsync(int pageNumber, int pageSize)
        {
            return await _dbContext.Quizzes
                .AsNoTracking()
                .OrderBy(q => q.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Quiz?> GetQuizByIdAsync(int id)
        {
            return await _dbContext.Quizzes.Include(q => q.Questions).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync(string searchText, int maxRows)
        {
            var query = _dbContext.Questions.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(q => q.QuestionText.ToLower().Contains(searchText.ToLower()));
            }
            return await query.Take(maxRows).ToListAsync();
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            var newQuiz = new Quiz { Title = quiz.Title, IsActive = quiz.IsActive, Questions = quiz.Questions.Where(q => q.Id == 0).ToList() };
            var existingQuestionIds = quiz.Questions.Where(q => q.Id != 0).Select(q => q.Id).ToList();
            var existingQuestions = await _dbContext.Questions.Where(q => existingQuestionIds.Contains(q.Id)).ToListAsync();
            newQuiz.Questions.AddRange(existingQuestions);

            await _dbContext.Quizzes.AddAsync(newQuiz);
            await _dbContext.SaveChangesAsync();
            return quiz;
        }

        public async Task<bool> UpdateQuizAsync(int id, Quiz quiz)
        {
            var quizToUpdate = await GetQuizByIdAsync(id);
            if (quizToUpdate is null)
            {
                return false;
            }
            quizToUpdate.Title = quiz.Title;
            quizToUpdate.Questions.RemoveAll(q => !quiz.Questions.Select(q => q.Id).Contains(q.Id));

            foreach (var newQuestion in quiz.Questions)
            {
                if (newQuestion.Id == 0 || !quizToUpdate.Questions.Any(q => q.Id == newQuestion.Id))
                {
                    quizToUpdate.Questions.Add(newQuestion);
                }
            }

            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SoftDeleteAsync(int quizId)
        {
            var quizToSoftDelete = await _dbContext.Quizzes.FindAsync(quizId);
            if (quizToSoftDelete is null)
            {
                return false;
            }
            quizToSoftDelete.IsActive = false;
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
