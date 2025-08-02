namespace QuizMaker.Application.DTOs
{
    public record BaseQuizDTO()
    {
        public int Id { get; set; }
        public required string Title { get; set; }
    }
}
