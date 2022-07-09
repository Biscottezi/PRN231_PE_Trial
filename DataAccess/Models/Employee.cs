using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Employee
    {
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public int? YearOfBirth { get; set; }
        public string JobTitle { get; set; }
        public string DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
