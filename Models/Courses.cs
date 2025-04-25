using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Courses
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(2)]
        public string Name { get; set; }
        [Range(0, 100)]
        public int Degree { get; set; }
        [Range(0, 100)]
        public int MinDegree { get; set; }
        public ICollection<Teachers> Teachers { get; set; } = new HashSet<Teachers>();
        public ICollection<StuCrsRes> StuCrsRes { get; set; } = new HashSet<StuCrsRes>();
        public Departments? Department { get; set; }
        [ForeignKey("Departments")]
        public int DepartmentId { get; set; }

    }
}
