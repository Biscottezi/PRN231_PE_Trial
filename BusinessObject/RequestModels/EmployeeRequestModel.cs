using System.ComponentModel.DataAnnotations;

namespace BusinessObject.RequestModels
{
    public class EmployeeRequestModel
    {
        [Required]
        [MinLength(9)]
        public string FullName { get; set; }
        
        [Required]
        public int? YearOfBirth { get; set; }
        
        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string JobTitle { get; set; }
        
        [Required]
        public string DepartmentId { get; set; }
    }
}