using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Project.Models;

namespace MVC_Project.Configrations
{
    public class CoursesConfigration : IEntityTypeConfiguration<Courses>
    {
        public void Configure(EntityTypeBuilder<Courses> C)
        {
            C.HasMany(sc => sc.StuCrsRes)
            .WithOne(c => c.Course)
            .HasForeignKey(c => c.Courseid).OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
