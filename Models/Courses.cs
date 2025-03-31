using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Courses
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public int MinDegree { get; set; }
        public ICollection<Teachers> Teachers { get; set; } = new HashSet<Teachers>();
        public ICollection<StuCrsRes> StuCrsRes { get; set; } = new HashSet<StuCrsRes>();
        public Departments? Department { get; set; }
        [ForeignKey("Departments")]
        public int DepartmentId { get; set; }

    }
}
