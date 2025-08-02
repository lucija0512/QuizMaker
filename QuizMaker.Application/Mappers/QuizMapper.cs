using QuizMaker.Application.DTOs;
using QuizMaker.Domain.Entities;

namespace QuizMaker.Application.Mappers
{
    public static class QuizMapper
    {
        public static QuizDTO MapEntityToQuizDto(this Quiz entity)
        {
            return new QuizDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                Questions = entity.Questions.Select(e => e.MapEntityToQuestionDto()).ToList()
            };
        }

        public static BaseQuizDTO MapEntityToBaseQuizDto(this Quiz entity)
        {
            return new BaseQuizDTO()
            {
                Id = entity.Id,
                Title = entity.Title,
            };
        }

        public static Quiz MapCreateDtoToEntity(this CreateQuizDTO dto)
        {
            return new Quiz
            {
                Title = dto.Title,
                Questions = dto.Questions.Select(e => e.MapQuestionDTOToEntity()).ToList(),
                IsActive = true
            };
        }

        public static Quiz MapUpdateDtoToEntity(this UpdateQuizDTO dto)
        {
            return new Quiz
            {
                Id = dto.Id,
                Title = dto.Title,
                Questions = dto.Questions.Select(e => e.MapQuestionDTOToEntity()).ToList(),
                IsActive = true
            };
        }
    }
}
