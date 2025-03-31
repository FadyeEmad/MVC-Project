using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Project.Models;

namespace MVC_Project.Configrations
{
    public class StudentsConfigrationcs : IEntityTypeConfiguration<Students>
    {
        public void Configure(EntityTypeBuilder<Students> S)
        {
            S.HasMany(sc => sc.StuCrsRes)
              .WithOne(s => s.Student)
              .HasForeignKey(s => s.Studentid).OnDelete(DeleteBehavior.NoAction); ;
        }
    }
}
