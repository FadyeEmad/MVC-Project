using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Teachers
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Salary { get; set; }
        public string Address { get; set; }
        public Courses? course { get; set; }
        public Departments? Department { get; set; }

        [ForeignKey("Courses")]
        public int CourseId { get; set; }
        [ForeignKey("Departments")]
        public int Departmentid { get; set; }
    }
}
