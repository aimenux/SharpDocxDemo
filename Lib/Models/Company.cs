using System;
using System.Collections.Generic;

namespace Lib.Models
{
    public class Company
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public DateTime? AverageBirthDate { get; set; }
        public int? AverageSalary { get; set; }
    }
}
