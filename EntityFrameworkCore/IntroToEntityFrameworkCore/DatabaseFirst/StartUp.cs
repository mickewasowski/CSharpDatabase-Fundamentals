using SoftUni.Data;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            using (var context = new SoftUniContext())
            {
                var result = GetEmployeesFullInformation(context);
                Console.WriteLine(result);
            }
        }
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var employees = context.Employees.OrderBy(x => x.EmployeeId).ToList();

            StringBuilder sb = new StringBuilder();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
