using Microsoft.EntityFrameworkCore;
using QuizMaker.Domain.Entities;
using QuizMaker.Infrastructure.EF.Configurations;

namespace QuizMaker.Infrastructure.EF
{
    public class QuizMakerDbContext : DbContext
    {
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }

        public QuizMakerDbContext(DbContextOptions<QuizMakerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new QuizEntityTypeConfiguration().Configure(modelBuilder.Entity<Quiz>());
            new QuestionEntityTypeConfiguration().Configure(modelBuilder.Entity<Question>());
        }
    }
}
