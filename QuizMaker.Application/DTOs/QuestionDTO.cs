namespace QuizMaker.Application.DTOs
{
    public record QuestionDTO
    {
        public int Id { get; set; }
        public required string QuestionText { get; set; }
        public required string CorrectAnswer { get; set; }
    }
}
