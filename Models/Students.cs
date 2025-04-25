using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Students
    {
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        [MinLength(2)]  
        [RegularExpression(@"^[A-Za-z\s]+$")]
        [Display(Name = "Full Name")]
        [UniqueName (ErrorMessage ="Name Must Be Unique")]
        public string Name { get; set; }

        [Required]
        [Range(1, 50)]
        public int Age { get; set; }
        public ICollection<StuCrsRes> StuCrsRes { get; set; } = new HashSet<StuCrsRes>();
        public Departments? Department  { get; set; }
        [ForeignKey("Departments")]
        [Required]
        [Display(Name= "Departament")]
        [Range (1,int.MaxValue)]
        public int Departmentid { get; set; }
    }
}
