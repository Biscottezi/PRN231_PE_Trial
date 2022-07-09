

#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public partial class Employee
    {
        
        public string EmployeeId { get; set; }
        
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
        public virtual Department Department { get; set; }
    }
}
