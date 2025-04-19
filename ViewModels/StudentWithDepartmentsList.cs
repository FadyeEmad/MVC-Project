using MVC_Project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace MVC_Project.ViewModels
{
    public class StudentWithDepartmentsList
    {
        public int Id { get; set; }
        //[DataType(DataType.Password)]
        [Display(Name="Student Name")]
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<StuCrsRes> StuCrsRes { get; set; } = new HashSet<StuCrsRes>();
        public Departments? Department { get; set; }
        [ForeignKey("Departments")]
        public int Departmentid { get; set; }
        public List<Departments> DepartmentList { get; set; }
    }
}
