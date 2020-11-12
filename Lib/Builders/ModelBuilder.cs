using Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lib.Builders
{
    public class ModelBuilder : IModelBuilder
    {
        public Company BuildCompany()
        {
            var employees = BuildEmployees();
            var averageSalary = employees.Count > 0
                    ? (int)employees.Average(e => e.Salary)
                    : (int?)null;
            var averageBirthDate = GetAverageBirthDate(employees);

            return new Company
            {
                Name = "Soft Corporation",
                Employees = employees,
                AverageSalary = averageSalary,
                AverageBirthDate = averageBirthDate
            };
        }

        private static List<Employee> BuildEmployees()
        {
            return new List<Employee>
            {
                new Employee
                {
                    Name = "Lawson Blackwell",
                    Salary = 96084500,
                    BirthDate = new DateTime(1967, 6, 18)
                },
                new Employee
                {
                    Name = "Keanu Lang",
                    Salary = 82349400,
                    BirthDate = new DateTime(1990, 10, 3)
                },
                new Employee
                {
                    Name = "Gladys Whitworth",
                    Salary = 6378000,
                    BirthDate = new DateTime(1986, 4, 24)
                },
                new Employee
                {
                    Name = "Hendrix Kenny",
                    Salary = 38634007,
                    BirthDate = new DateTime(1989, 9, 13)
                },
                new Employee
                {
                    Name = "Nabiha Alvarado",
                    Salary = 17170000,
                    BirthDate = new DateTime(1971, 7, 26)
                },
                new Employee
                {
                    Name = "Rudi Lawrence",
                    Salary = 325365189,
                    BirthDate = new DateTime(1991, 7, 4)
                },
            };
        }

        private static DateTime? GetAverageBirthDate(ICollection<Employee> employees)
        {
            var dates = employees
                .Where(e => e.BirthDate.HasValue)
                .Select(e => e.BirthDate.Value)
                .ToList();

            if (dates.Count == 0)
            {
                return null;
            }

            return new DateTime((long)dates.Aggregate<DateTime, double>(0, (current, date) => current + date.Ticks / dates.Count));
        }
    }
}
