using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Wasleh.Domain.Entities;

namespace Wasleh.Presistence.Configuration;

public class FacultyConfig : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.University).WithMany(x => x.Faculties).HasForeignKey(x => x.UniversityId);
    }
}
