namespace QuizMaker.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public required string QuestionText { get; set; }
        public required string CorrectAnswer { get; set; }
        public bool IsActive { get; set; }
        public List<Quiz> Quizes { get; set; } = [];
    }
}
