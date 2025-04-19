using MVC_Project.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.ViewModels
{
    public class StudentFilterViewModel
    {
        public List<Students> Students { get; set; }
        public string? SearchName { get; set; }
        public int? DepartmentId { get; set; }
        public List<Departments> Departments { get; set; } = new List<Departments>();
        // Pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int StudentsCount { get; set; }
        public int SelectedPageSize { get; set; } = 10;
    }
}
