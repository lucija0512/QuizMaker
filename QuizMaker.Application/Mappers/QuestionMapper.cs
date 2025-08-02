using QuizMaker.Application.DTOs;
using QuizMaker.Domain.Entities;

namespace QuizMaker.Application.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionDTO MapEntityToQuestionDto(this Question entity)
        {
            return new QuestionDTO
            {
                Id = entity.Id,
                QuestionText = entity.QuestionText,
                CorrectAnswer = entity.CorrectAnswer
            };
        }

        public static Question MapQuestionDTOToEntity(this QuestionDTO dto)
        {
            return new Question
            {
                Id = dto.Id,
                QuestionText = dto.QuestionText,
                CorrectAnswer = dto.CorrectAnswer,
                IsActive = true
            };
        }
    }
}
