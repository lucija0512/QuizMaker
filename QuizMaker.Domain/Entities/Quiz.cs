namespace QuizMaker.Domain.Entities
{
    public class Quiz
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public bool IsActive { get; set; }
        public List<Question> Questions { get; set; } = [];
    }
}
