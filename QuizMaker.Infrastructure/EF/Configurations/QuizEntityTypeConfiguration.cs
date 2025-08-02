using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QuizMaker.Domain.Entities;

namespace QuizMaker.Infrastructure.EF.Configurations
{
    public class QuizEntityTypeConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quiz");

            builder.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.HasQueryFilter(e => e.IsActive);

            builder.HasMany(e => e.Questions)
                   .WithMany(e => e.Quizes)
                   .UsingEntity<QuizQuestion>();
        }
    }
}
