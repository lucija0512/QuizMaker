namespace QuizMaker.Application.DTOs
{
    public record CreateQuizDTO
    {
        public required string Title { get; set; }
        public List<QuestionDTO> Questions { get; set; } = [];
    }
}
