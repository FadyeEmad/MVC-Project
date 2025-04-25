using MVC_Project.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.ViewModels
{
    public class CourseWithStudentViewModel
    {
        public List<Courses> Courses { get; set; }
        public int? CourseId { get; set; }
        public List<StudemtsWithGradeViewModel> Students { get; set; }
        public string? SearchName { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int CourseCount { get; set; }
        public int SelectedPageSize { get; set; } = 10;


    }
}
