using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasleh.Domain.Entities;

namespace Wasleh.Presistence.Configuration;

public class ReplyConfig : IEntityTypeConfiguration<Reply>
{
    public void Configure(EntityTypeBuilder<Reply> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Answer).WithMany(x => x.Replies).HasForeignKey(x => x.AnswerId).OnDelete(DeleteBehavior.NoAction);
    }
}
