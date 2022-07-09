using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace DataAccess.Models
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<Employee>();
        }

        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentDescription { get; set; }
        [JsonIgnore]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
