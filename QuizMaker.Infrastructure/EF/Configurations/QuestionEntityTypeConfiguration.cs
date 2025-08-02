using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizMaker.Domain.Entities;

namespace QuizMaker.Infrastructure.EF.Configurations
{
    public class QuestionEntityTypeConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Question");

            builder.Property(e => e.QuestionText)
                    .IsRequired()
                    .HasMaxLength(400);

            builder.Property(e => e.CorrectAnswer)
                    .IsRequired()
                    .HasMaxLength(200);

            builder.HasQueryFilter(e => e.IsActive);
        }
    }
}
