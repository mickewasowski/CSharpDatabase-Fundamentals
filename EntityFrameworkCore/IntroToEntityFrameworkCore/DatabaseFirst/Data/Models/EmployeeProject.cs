using System;
using System.Collections.Generic;

namespace SoftUni.Data.Models
{
    public class EmployeeProject
    {
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }

        public Project Project { get; set; }
        public int ProjectId { get; set; }
    }
}
