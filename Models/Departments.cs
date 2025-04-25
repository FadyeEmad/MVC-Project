using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class Departments
    {
        public int Id { get; set; }
        [Required]
        [StringLength(25)]
        [MinLength(2)]
        [Display(Name = "Department Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(25)]
        [MinLength(2)]
        [RegularExpression(@"^[A-Za-z\s]+$")]
        [Display(Name = "Manager Name")]
        public string MgrName { get; set; }
        public ICollection<Teachers> Teachers { get; set; } = new HashSet<Teachers>();
        public ICollection<Students> Students { get; set; } = new HashSet<Students>();
        public ICollection<Courses> Courses { get; set; } = new HashSet<Courses>();

        
    }
}
