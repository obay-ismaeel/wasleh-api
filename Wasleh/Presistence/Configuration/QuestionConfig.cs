using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasleh.Domain.Entities;

namespace Wasleh.Presistence.Configuration;

public class QuestionConfig : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.User).WithMany(x => x.Questions).HasForeignKey(x => x.UserId);
    }
}
