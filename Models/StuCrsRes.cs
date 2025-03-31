using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models
{
    public class StuCrsRes
    {
        [ForeignKey("Students")]
        public int Studentid { get; set; }
        
        [ForeignKey("Courses")]
        public int Courseid { get; set; }
        public int Grade { get; set; }
        public Students? Student { get; set; }
        public Courses? Course { get; set; }
    }
}
