namespace MVC_Project.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MgrName { get; set; }
        public ICollection<Teachers> Teachers { get; set; } = new HashSet<Teachers>();
        public ICollection<Students> Students { get; set; } = new HashSet<Students>();
        public ICollection<Courses> Courses { get; set; } = new HashSet<Courses>();


    }
}
