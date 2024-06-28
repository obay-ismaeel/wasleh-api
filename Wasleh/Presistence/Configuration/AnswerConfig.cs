using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasleh.Domain.Entities;

namespace Wasleh.Presistence.Configuration;

public class AnswerConfig : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Question).WithMany(x => x.Answers).HasForeignKey(x => x.QuestionId).OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(x => x.User).WithMany(x => x.Answers).HasForeignKey(x => x.UserId);
    }
}
