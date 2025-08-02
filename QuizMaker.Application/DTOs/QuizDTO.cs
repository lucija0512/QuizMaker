namespace QuizMaker.Application.DTOs
{
    public record QuizDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public List<QuestionDTO> Questions { get; set; } = [];
    }
}
