using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasleh.Domain.Entities;

namespace Wasleh.Presistence.Configuration;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasMany(x => x.Questions).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        builder.HasMany(x => x.Answers).WithOne(x => x.User).HasForeignKey(x => x.UserId);
    }
}
