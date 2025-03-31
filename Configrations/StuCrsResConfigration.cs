using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MVC_Project.Models;

namespace MVC_Project.Configrations
{
    public class StuCrsResConfigration : IEntityTypeConfiguration<StuCrsRes>
    {
        public void Configure(EntityTypeBuilder<StuCrsRes> SCR)
        {
            SCR.HasKey(scr => new{scr.Courseid, scr.Studentid});
        }
    }
}
