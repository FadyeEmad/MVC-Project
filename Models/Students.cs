using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class Students
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public ICollection<StuCrsRes> StuCrsRes { get; set; } = new HashSet<StuCrsRes>();
        public Departments? Department  { get; set; }
        [ForeignKey("Departments")]
        public int Departmentid { get; set; }
    }
}
