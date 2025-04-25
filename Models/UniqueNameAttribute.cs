using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            string NewName= value.ToString();
            SystemDbContext context = new SystemDbContext();
            Students StudentFromReq = context.Students.FirstOrDefault(s=> s.Name== NewName);
            if (StudentFromReq != null)
            {
                return new ValidationResult(ErrorMessage);
            }
            return ValidationResult.Success;
        }
    }
}
