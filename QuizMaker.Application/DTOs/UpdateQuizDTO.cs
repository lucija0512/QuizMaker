namespace QuizMaker.Application.DTOs
{
    public record UpdateQuizDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public List<QuestionDTO> Questions { get; set; } = [];
    }
}
